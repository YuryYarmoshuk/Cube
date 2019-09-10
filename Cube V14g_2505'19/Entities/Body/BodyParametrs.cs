/*
           ________________
          /|              /|
         / |             / |
        /  |            /  |
       /   |           /   |
      /    |          /    |
     /     |_ _ _ _ _/_ _ _|
   H|---------------|      /
   E|     /         |     /T
   I|    /          |    /H
   G|   /           |   /G
   H|  /            |  /N
   T| /             | /E
    |/______________|/L
           WIDTH

*/

namespace Cube_V11
{
    public class BodyParametrs
    {
        private double V;
        private double Lenght;
        private double Width;
        private double Height;

        public BodyParametrs(double bodyWidth, double bodyHeight, double bodyLenght)
        {
            Width = bodyWidth;
            Height = bodyHeight;
            Lenght = bodyLenght;
            V = bodyWidth * bodyHeight * bodyLenght;
        }

        public double GetWidth() { return Width; }
        public double GetHeight() { return Height; }
        public double GetLenght() { return Lenght; }
        public double GetV() { return V; }
    }
}
