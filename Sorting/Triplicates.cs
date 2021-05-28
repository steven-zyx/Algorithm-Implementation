using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting
{
    public class Triplicates
    {
        public int FindCommon(int[] s1, int[] s2, int[] s3)
        {
            MergeSortImproved client = new MergeSortImproved();
            s1 = client.Sort(s1);
            s2 = client.Sort(s2);
            s3 = client.Sort(s3);

            int minLength = Math.Min(Math.Min(s1.Length, s2.Length), s3.Length);


            int i1 = 0;
            int i2 = 0;
            int i3 = 0;
            while (i1 < minLength && i2 < minLength && i3 < minLength)
            {
                if (s1[i1] < s2[i2])
                {
                    i1++;
                }
                else if (s1[i1] > s2[i2])
                {
                    i2++;
                }
                else
                {
                    if (s3[i3] < s2[i2])
                    {
                        i3++;
                    }
                    else if (s3[i3] > s2[i2])
                    {
                        i1++;
                        i2++;
                    }
                    else
                    {
                        return s3[i3];
                    }
                }
            }
            return int.MinValue;
        }
    }
}
