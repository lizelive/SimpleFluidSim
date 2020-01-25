using System;
using System.Linq;
using static System.MathF;
namespace SimpleFluidSim
{
    class Program
    {
        const int Size = 128;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var world = new Node[Size, Size];
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    world[x, y] = new Node();
                }
            }

            for (int x = 1; x < Size - 1; x++)
            {
                for (int y = 1; y < Size - 1; y++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            world[dx, dy] = new Node();
                        }
                    }

                    world[x, y] = new Node();
                }
            }
        }
    }

    public class Node : IComparable<Node>
    {
        // how sticky is water
        const float FlowRatio = 1 / 6f;

        public float terrainHeight;
        public float amount;
        public float waterSurfaceHeight => terrainHeight + amount;
        public Node[] neighbors;


        public void ComputeFlow()
        {
            var flowTo = neighbors.Min();
            var flowAmmount = waterSurfaceHeight - flowTo.waterSurfaceHeight;
            flowAmmount = FlowRatio * Min(amount, flowAmmount);

            // only flow on postive
            if (flowAmmount > float.Epsilon)
            {
                amount -= flowAmmount;
                flowTo.amount += flowAmmount;
            }

        }

        public int CompareTo(Node other)
        {
            return waterSurfaceHeight.CompareTo(other.waterSurfaceHeight);
        }
    }
}
