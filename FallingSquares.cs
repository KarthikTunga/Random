using System;
using System.Collections.Generic;

namespace FallingSquares
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(int.MaxValue);
                var input = new int[,]{{1,10},{10,20},{20,30},{2,10},{3,10}};
                var ans = new Solution().FallingSquares(input);
                Console.WriteLine(string.Join(", ", ans));
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    class Node
    {
        public int low;

        public int high;

        public int mid;

        public int height;

        public Node left;

        public Node right;


        public Node(int start, int end, int height)
        {
            this.low = start;
            this.high = end;
            this.mid = low + ((high - low) / 2);
            this.height = height;
        }

        public int FindMax(int start, int end)
        {
            if(this.left == null) return this.height;

            int ans = 0;
            if(start <= this.left.high) ans = Math.Max(ans, this.left.FindMax(start, Math.Min(this.mid, end)));
            if(end >= this.right.low) ans = Math.Max(ans, this.right.FindMax(Math.Max(mid + 1, start), end));
            return ans;
        }

        public void Insert(int start, int end, int newHeight)
        {
            if(start <= this.low && end >= this.high)
            {
                if(newHeight > this.height)
                {
                    this.Propagate(newHeight);
                }
            }
            else
            {
                if(this.left == null)
                {
                    this.left = new Node(this.low, this.mid, this.height);
                    this.right = new Node(mid + 1, this.high, this.height);
                }

                if(start <= this.left.high) this.left.Insert(start, Math.Min(this.mid, end), newHeight);
                if(end >= this.right.low) this.right.Insert(Math.Max(start, this.mid+1), end, newHeight);
                this.height = Math.Max(this.left.height, this.right.height);
            }
        }

        private void Propagate(int newHeight)
        {
            this.height = newHeight;
            if(this.left != null)
            {
                this.left.Propagate(newHeight);
                this.right.Propagate(newHeight);
            }
        }

        public void Print()
        {
            Console.WriteLine($"({this.low}, {this.high}) => {this.height}");
            if(this.left != null)
            {
                this.left.Print();
                this.right.Print();
            }
        }
    }

    class Solution
    {
        public IList<int> FallingSquares(int[,] positions) 
        {
            Node root = new Node(0, 100000000, 0);
            var ans = new List<int>();
            int max = 0;
            for(int i = 0; i < positions.GetLength(0); ++i)
            {
                int start = positions[i, 0];
                int end = start + positions[i, 1] - 1;
                int height = positions[i, 1];

                int newHeight = root.FindMax(start, end) + height;
                Console.WriteLine($"Max({start}, {end}) => {newHeight - height}");
                max = Math.Max(max, newHeight);
                ans.Add(max);

                root.Insert(start, end, newHeight);
            }

            return ans;
        }
    }
}
