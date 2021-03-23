using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Google.Cloud.Vision.V1;
using Newtonsoft.Json.Linq;

namespace TheDifferenciatorUI
{
    public static class GoogleWetDetect
    {
        /*
         * Wet Detect assumes that folder is semi harmoginous with similar lighting conditions and image adjustments/postion/camera
         * best results from using on the same folder / hole / photo session / collection folder.
         * */


        public class GoogleResult
        {
            float result;
            string imgPath;
            float avgBlack;
            float avgBlue;
            float AvgSat;

            public float Result { get => result; set => result = value; }
            public string ImgPath { get => imgPath; set => imgPath = value; }
            public float AvgBlack { get => avgBlack; set => avgBlack = value; }
            public float AvgBlue { get => avgBlue; set => avgBlue = value; }
            public float AvgSat1 { get => AvgSat; set => AvgSat = value; }

            public float GetWetScore()
            {
                var temp = result - avgBlack;
                return temp * AvgSat;
            }
        }

        public static List<string> MyGoogleVision(string FolderPath)
        {
            //returns list for writing to csv
            List<string> result = new List<string>();


            List<GoogleResult> resultArray = new List<GoogleResult>();
            List<GoogleResult> divergeArray = new List<GoogleResult>();
            List<string> imagesInFolder = new List<string>();
            string credential_path = @"C:\Users\Administrator\Pictures\ocrtest-296704-720de5204d3d.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
            var client = ImageAnnotatorClient.Create();


            string folderpath = FolderPath;
            var actualpath = Path.GetDirectoryName(folderpath + @"\");
            var files = Directory.EnumerateFiles(actualpath, "*.jpg").OrderBy(filename => filename);

            imagesInFolder = files.ToList<string>();
            using (NaturalComparer comparer = new NaturalComparer())
            {
                imagesInFolder.Sort(comparer);
            }

            float totalpixelFraction = 0;
            float averagePixelRatio = 0;
            
            foreach (string img in imagesInFolder)
            {
                GoogleResult imgResult = new GoogleResult();
                imgResult.ImgPath = img;

                Google.Cloud.Vision.V1.Image gImage = Google.Cloud.Vision.V1.Image.FromFile(img);
                var response = client.DetectImageProperties(gImage);
                
                JObject joResponse = JObject.Parse(response.ToString());
                JObject ojObject = (JObject)joResponse["dominantColors"];
                JArray array = (JArray)ojObject["colors"];

                int responseLength = array.Count;

                Console.WriteLine("Color Response = " + joResponse);
                
                int count = 0;
                float imgResulPixelFraction = 0;
                foreach (var color in array)
                {
                    imgResulPixelFraction = imgResulPixelFraction + float.Parse(array[count]["pixelFraction"].ToString());
                    count++;
                }

                totalpixelFraction = totalpixelFraction + imgResulPixelFraction;

                imgResult.Result = imgResulPixelFraction;
                imgResult.AvgBlack = ImgKit.AveragePixelsBlack(new Bitmap(img).Width, new Bitmap(img).Height, new Bitmap(img));
                imgResult.AvgBlue = ImgKit.AverageBlue(new Bitmap(img).Width, new Bitmap(img).Height, new Bitmap(img));
                imgResult.AvgSat1 = ImgKit.AverageSat(new Bitmap(img).Width, new Bitmap(img).Height, new Bitmap(img));
                resultArray.Add(imgResult);

            }

            averagePixelRatio = totalpixelFraction / resultArray.Count;
            //findoutblue
            //gotta find the deviators in the list remove them and add them to a new list for scaling with the old list min / max
            foreach (GoogleResult res in resultArray)
            {
                //get vars from objects
                var aMin = resultArray.Min(arr => arr.Result);

                //may need to adjust the deviation
                float deviationMore = averagePixelRatio + (float)0.07;
                float deviationLess = averagePixelRatio - (float)0.07;

                if (res.Result > deviationMore)
                {
                    divergeArray.Add(res);
                    //remove;
                }
                else if (res.Result < deviationLess)
                {
                    divergeArray.Add(res);
                    //remove;
                }
            }
            foreach (var item in divergeArray)
            {
                resultArray.Remove(item);
            }
            List<GoogleResult> newdivergeArray = new List<GoogleResult>();
            //scale the div results with the new lists min / max
            if (divergeArray.Count != 0)
            {
                newdivergeArray = GetScaling(divergeArray, resultArray.Min(rArr => rArr.Result), resultArray.Max(rArrs => rArrs.Result));
            }


            resultArray.AddRange(newdivergeArray);

            //recalculate the new average
            float newTotal = resultArray.Sum(rArr => rArr.Result);
            float newAverage = newTotal / resultArray.Count;

            //if a result is too large because it deviates more than avg + (avg - min) then scale those results with the range of the others {min --> max(not counting the deviated results}
            //https://stats.stackexchange.com/questions/281162/scale-a-number-between-a-range
            //might need more thought but once all scaled - if something is more than average it will be marked wet

            using (NaturalComparer comparer = new NaturalComparer())
            {
                resultArray.Sort((a, b) => b.ImgPath.CompareTo(a.ImgPath)); // sort and keep sorted list in list itself
            }
            float[] realResults = new float[resultArray.Count];
            int countUp = 0;
            foreach(var res in resultArray)
            {
                realResults[countUp] = res.GetWetScore();
                countUp++;
            }
            float median = GetMedian(realResults);
            float average = realResults.Sum() / resultArray.Count;

            foreach (var res in resultArray)
            {
                string printOut;

                if (res.GetWetScore() > average)
                {
                    //wet
                    printOut = res.ImgPath + ",Wet," + res.Result + "," + res.AvgBlack + "," + res.AvgBlue + "," + res.AvgSat1;
                    result.Add(printOut);
                }
                else
                {
                    //dry
                    printOut = res.ImgPath + ",Dry," + res.Result + "," + res.AvgBlack + "," + res.AvgBlue + "," + res.AvgSat1;
                    result.Add(printOut);
                }
                //multiply by 
                //CALIBRATION ideaa - if u have more wet or dry than the other go over either wet or dry again without including the others and see what result displays
            }

            return result;

        }

        public static List<GoogleResult> GetScaling(List<GoogleResult> arr, float min, float max)
        {
            float m = (max - min) / (arr.Max(rArr => rArr.Result) - arr.Min(rArr => rArr.Result));
            float c = min - arr.Min(rArr => rArr.Result) * m;
            List<GoogleResult> newarr = arr;
            for (int i = 0; i < arr.Count; i++)
                newarr[i].Result = m * arr[i].Result + c;
            return newarr;
        }
        public static float GetMedian(this IEnumerable<float> source)
        {
            // Create a copy of the input, and sort the copy
            float[] temp = source.ToArray();
            Array.Sort(temp);
            int count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
            else if (count % 2 == 0)
            {
                // count is even, average two middle elements
                float a = temp[count / 2 - 1];
                float b = temp[count / 2];
                return (a + b) / 2;
            }
            else
            {
                // count is odd, return the middle element
                return temp[count / 2];
            }
        }
        public static float GetMode(this IEnumerable<float> list)
        {
            // Initialize the return value
            float mode = default(float);
            // Test for a null reference and an empty list
            if (list != null && list.Count() > 0)
            {
                // Store the number of occurences for each element
                Dictionary<int, float> counts = new Dictionary<int, float>();
                // Add one to the count for the occurence of a character
                foreach (int element in list)
                {
                    if (counts.ContainsKey(element))
                        counts[element]++;
                    else
                        counts.Add(element, 1);
                }
                // Loop through the counts of each element and find the 
                // element that occurred most often
                float max = 0;
                foreach (KeyValuePair<int, float> count in counts)
                {
                    if (count.Value > max)
                    {
                        // Update the mode
                        mode = count.Key;
                        max = count.Value;
                    }
                }
            }
            return mode;
        }
    
    public class NaturalComparer : Comparer<string>, IDisposable
        {
            private Dictionary<string, string[]> table;

            public NaturalComparer()
            {
                table = new Dictionary<string, string[]>();
            }

            public void Dispose()
            {
                table.Clear();
                table = null;
            }


            public override int Compare(string x, string y)
            {
                if (x == y)
                {
                    return 0;
                }
                string[] x1, y1;
                if (!table.TryGetValue(x, out x1))
                {
                    x1 = Regex.Split(x.Replace(" ", ""), "([0-9]+)");
                    table.Add(x, x1);
                }
                if (!table.TryGetValue(y, out y1))
                {
                    y1 = Regex.Split(y.Replace(" ", ""), "([0-9]+)");
                    table.Add(y, y1);
                }

                for (int i = 0; i < x1.Length && i < y1.Length; i++)
                {
                    if (x1[i] != y1[i])
                    {
                        return PartCompare(x1[i], y1[i]);
                    }
                }
                if (y1.Length > x1.Length)
                {
                    return 1;
                }
                else if (x1.Length > y1.Length)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            private static int PartCompare(string left, string right)
            {
                int x, y;
                if (!int.TryParse(left, out x))
                {
                    return left.CompareTo(right);
                }

                if (!int.TryParse(right, out y))
                {
                    return left.CompareTo(right);
                }

                return x.CompareTo(y);
            }
        }

    }


}
