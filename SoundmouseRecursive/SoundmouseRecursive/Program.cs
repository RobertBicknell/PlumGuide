using System.Collections.Generic;
using System;
using System.Linq;

namespace SoundmouseRecursive
{

    struct Range {
        public int Start;
        public int End;
    }
    class Program
    {
        static List<List<Range>> AllRanges;
        static List<List<int>> Matrix;

        static bool lowerRowContainsRange(int rowId, Range requiredRange, int count, int target)
        {
            if (count == target) return true; //all required subranges were found 
            if (rowId + 1== Matrix.Count) return false; //not reached targte and un out of rows, so required lower range is not present
            var rangesInLowerRow = AllRanges[rowId + 1];
            foreach (var rangeInLowerRow in rangesInLowerRow) {
                if (!(requiredRange.Start >= rangeInLowerRow.Start && requiredRange.End <= rangeInLowerRow.End)) return false;
                return lowerRowContainsRange(rowId + 1, requiredRange, count + 1, target);
            }
            return false;
        }

        static List<List<Range>> getRangesInMatrix(List<List<int>> arr) {
            var result = new List<List<Range>>();
            foreach (var row in arr) {
                var ranges = new List<Range>();
                var last = 0;
                var lastIdx = 0;
                var rangeStart = 0;
                for (var idx = 0; idx < row.Count; idx++) {
                    if (row[idx] == 1 && last == 0) {
                        rangeStart = idx;
                    }
                    else if (row[idx] == 0 && last == 1) {
                        ranges.Add(new Range() {
                            Start = rangeStart,
                            End = lastIdx
                        });
                    }
                    last = row[idx];
                    lastIdx = idx;
                }
                if (last == 1) {
                    ranges.Add(new Range()
                    {
                        Start = rangeStart,
                        End = row.Count - 1
                    });
                }
                result.Add(ranges);
            }
            return result;
        }

        public static int exploreSubranges(int idx, Range range) {
            var result = 0;
            for (var loopStart = range.Start; loopStart <= range.End; loopStart++)
            {
                for (var loopEnd = range.End; loopEnd >= loopStart; loopEnd--)
                {
                    var targetSquareSize = 1 + loopEnd - loopStart;
                    if (targetSquareSize <= result) break; //optimization
                    var loopRange = new Range() {
                        Start = loopStart,
                        End = loopEnd
                    };
                    if (lowerRowContainsRange(idx, loopRange, 1, targetSquareSize)) {
                        result = Math.Max(result, targetSquareSize);
                    }
                }
            }
            return result;
        }

        public static int largestMatrix(List<List<int>> arr)
        {
            Matrix = arr;
            AllRanges = getRangesInMatrix(Matrix); 
            var result = 0;
            for (var idx =0; idx < arr.Count; idx++)
            {
                if (arr.Count - idx <= result) continue; //optimization
                foreach (var range in AllRanges[idx]) {
                    result = Math.Max(exploreSubranges(idx, range), result);
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            var x = 0;
            var testrow1 = new List<int> { 1, 1, 1, 1 };
            var testrow2 = new List<int> { 1, 1, 1, 1 };
            var testrow3 = new List<int> { 1, 1, 1, 1 };
            var testrow4 = new List<int> { 1, 1, 1, 1 };

            var testMtx = new List<List<int>>();

            /*
            testMtx.Add(testrow1);
            testMtx.Add(testrow2);
            testMtx.Add(testrow3);
            testMtx.Add(testrow4);
            x = largestMatrix(testMtx);

            testrow1 = new List<int> { 0, 1, 1, 1 };
            testrow2 = new List<int> { 1, 1, 1, 1 };
            testrow3 = new List<int> { 1, 1, 1, 1 };
            testrow4 = new List<int> { 1, 1, 1, 1 };

            testMtx = new List<List<int>>();

            testMtx.Clear();
            testMtx.Add(testrow1);
            testMtx.Add(testrow2);
            testMtx.Add(testrow3);
            testMtx.Add(testrow4);
            x = largestMatrix(testMtx);


            testrow1 = new List<int> { 1, 1, 1, 1 };
            testrow2 = new List<int> { 1, 1, 1, 1 };
            testrow3 = new List<int> { 1, 1, 1, 1 };
            testrow4 = new List<int> { 1, 1, 1, 0 };

            testMtx = new List<List<int>>();

            testMtx.Clear();
            testMtx.Add(testrow1);
            testMtx.Add(testrow2);
            testMtx.Add(testrow3);
            testMtx.Add(testrow4);
            x = largestMatrix(testMtx);


            testrow1 = new List<int> { 0, 1, 1, 1 };
            testrow2 = new List<int> { 1, 1, 1, 1 };
            testrow3 = new List<int> { 1, 1, 1, 1 };
            testrow4 = new List<int> { 1, 1, 1, 0 };

            testMtx = new List<List<int>>();

            testMtx.Clear();
            testMtx.Add(testrow1);
            testMtx.Add(testrow2);
            testMtx.Add(testrow3);
            testMtx.Add(testrow4);
            x = largestMatrix(testMtx);



            testrow1 = new List<int> { 0, 0, 0, 0 };
            testrow2 = new List<int> { 0, 0, 0, 0 };
            testrow3 = new List<int> { 0, 0, 0, 0 };
            testrow4 = new List<int> { 0, 0, 0, 0 };

            testMtx = new List<List<int>>();

            testMtx.Clear();
            testMtx.Add(testrow1);
            testMtx.Add(testrow2);
            testMtx.Add(testrow3);
            testMtx.Add(testrow4);
            x = largestMatrix(testMtx);



            testrow1 = new List<int> { 1, 1, 0, 0 };
            testrow2 = new List<int> { 1, 1, 0, 0 };
            testrow3 = new List<int> { 1, 1, 0, 0 };
            testrow4 = new List<int> { 1, 1, 0, 0 };

            testMtx = new List<List<int>>();

            testMtx.Clear();
            testMtx.Add(testrow1);
            testMtx.Add(testrow2);
            testMtx.Add(testrow3);
            testMtx.Add(testrow4);
            x = largestMatrix(testMtx);



            testrow1 = new List<int> { 1, 1, 0, 0 };
            testrow2 = new List<int> { 1, 1, 0, 0 };
            testrow3 = new List<int> { 0, 1, 0, 0 };
            testrow4 = new List<int> { 0, 0, 0, 0 };

            testMtx = new List<List<int>>();

            testMtx.Clear();
            testMtx.Add(testrow1);
            testMtx.Add(testrow2);
            testMtx.Add(testrow3);
            testMtx.Add(testrow4);
            x = largestMatrix(testMtx);


            testrow1 = new List<int> { 0, 1, 1, 0 };
            testrow2 = new List<int> { 0, 1, 1, 0 };
            testrow3 = new List<int> { 0, 1, 0, 0 };
            testrow4 = new List<int> { 0, 1, 0, 0 };

            testMtx = new List<List<int>>();

            testMtx.Clear();
            testMtx.Add(testrow1);
            testMtx.Add(testrow2);
            testMtx.Add(testrow3);
            testMtx.Add(testrow4);
            x = largestMatrix(testMtx);


            testrow1 = new List<int> { 0, 1, 0, 0 };
            testrow2 = new List<int> { 0, 0, 0, 0 };
            testrow3 = new List<int> { 0, 0, 0, 0 };
            testrow4 = new List<int> { 1, 0, 0, 0 };

            testMtx = new List<List<int>>();

            testMtx.Clear();
            testMtx.Add(testrow1);
            testMtx.Add(testrow2);
            testMtx.Add(testrow3);
            testMtx.Add(testrow4);
            x = largestMatrix(testMtx);
            */


            testrow1 = new List<int> { 1, 1, 1, 1 , 1, 1, 1, 1 , 1, 1, 1, 1 , 0, 0, 0, 0 };
            testrow2 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 };
            testrow3 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
            testrow4 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var testrow5 = new List<int> { 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            var testrow6 = new List<int> { 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            var testrow7 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0 };
            var testrow8 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var testrow9 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var testrow10 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 };
            var testrow11 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0 };
            var testrow12 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var testrow13 = new List<int> { 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1 };
            var testrow14 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0 };
            var testrow15 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 };
            var testrow16 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 };

            testMtx = new List<List<int>>();

            testMtx.Add(testrow1);
            testMtx.Add(testrow2);
            testMtx.Add(testrow3);
            testMtx.Add(testrow4);
            testMtx.Add(testrow5);
            testMtx.Add(testrow6);
            testMtx.Add(testrow7);
            testMtx.Add(testrow8);
            testMtx.Add(testrow9);
            testMtx.Add(testrow10);
            testMtx.Add(testrow11);
            testMtx.Add(testrow12);
            testMtx.Add(testrow13);
            testMtx.Add(testrow14);
            testMtx.Add(testrow15);
            testMtx.Add(testrow16);


            x = largestMatrix(testMtx);



        }


    }
}
