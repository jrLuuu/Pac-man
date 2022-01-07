using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace endOfTerm
{
    public struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 front, Vector2 back)
        {
            return new Vector2(front.x + back.x, front.y + back.y);
        }

        public static Vector2 operator +(Vector2 front, int value)
        {
            return new Vector2(front.x + value, front.y + value);
        }

        public static Vector2 operator -(Vector2 front, Vector2 back)
        {
            return new Vector2(front.x - back.x, front.y - back.y);
        }

        public static Vector2 operator -(Vector2 front, int value)
        {
            return new Vector2(front.x - value, front.y - value);
        }

        public static Vector2 operator -(Vector2 front)
        {
            return new Vector2(-front.x, -front.y);
        }

        public static Vector2 operator *(Vector2 front, int back)
        {
            return new Vector2(front.x * back, front.y * back);
        }

        public static bool operator ==(Vector2 front, Vector2 back)
        {
            return (front.x == back.x && front.y == back.y);
        }

        public static bool operator !=(Vector2 front, Vector2 back)
        {
            return !(front == back);
        }

        public static bool operator >=(Vector2 front, Vector2 back)
        {
            return (front.x >= back.x && front.y >= back.y);
        }

        public static bool operator <=(Vector2 front, Vector2 back)
        {
            return (front.x <= back.x && front.y <= back.y);
        }

        public static bool operator >(Vector2 front, Vector2 back)
        {
            return (front.x > back.x && front.y > back.y);
        }

        public static bool operator <(Vector2 front, Vector2 back)
        {
            return (front.x < back.x && front.y < back.y);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 vector && x == vector.x && y == vector.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }
    }
}
