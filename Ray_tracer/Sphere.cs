using System;

namespace rt
{
    public class Sphere : Geometry
    {
        private Vector Center { get; set; }
        private double Radius { get; set; }

        public Sphere(Vector center, double radius, Material material, Color color) : base(material, color)
        {
            Center = center;
            Radius = radius;
        }

        public Vector GetCenter()
        {
            return Center;
        }
        
        /**
         * http://www.ambrsoft.com/TrigoCalc/Sphere/SpherLineIntersection_.htm
         * https://www.youtube.com/watch?v=HFPlKQGChpE - ray intersection with sphere
         */
        public override Intersection GetIntersection(Line line, double minDist, double maxDist)
        {
            // t denotes the time where the ray is closest to the center of the sphere (perpendicular)
            // also if the ray intersects the sphere in two points, both are equally distance from t and we'll call
            // the time it takes to reach them t1 and t2
            var t = (Center - line.X0) * line.Dx;
            
            // p denotes the point on the ray after time t
            var p = line.CoordinateToPosition(t);

            // y denotes the lenght between the center of the sphere and p
            var y = (Center - p).Length();

            // x is the distance between p and p1,p2 - the points reached after t1 and t2
            var x = Math.Sqrt(Radius * Radius - y * y);
        
            // we are only interested in the first intersection with the sphere - the minimal t
            // if x is 0 then t1 is t
            var t1 = t - x;
            
            if (t > minDist && t < maxDist && y <= Radius)
            {
                return new Intersection(true, true, this, line, t1);
            }
            return new Intersection();
        }

        public override Vector Normal(Vector v)
        {
            var n = v - Center;
            n.Normalize();
            return n;
        }
    }
}