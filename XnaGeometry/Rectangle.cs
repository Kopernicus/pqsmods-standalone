#region License
/*
MIT License
Copyright © 2006 The Mono.Xna Team

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

using System;
using System.Globalization;
using System.ComponentModel;

namespace XnaGeometry
{

    public struct Rectangle : IEquatable<Rectangle>
    {
        #region Private Fields

        private static Rectangle emptyRectangle = new Rectangle();

        #endregion Private Fields


        #region Public Fields

        public Int32 X;
        public Int32 Y;
        public Int32 Width;
        public Int32 Height;

        #endregion Public Fields


        #region Public Properties

        public static Rectangle Empty
        {
            get { return emptyRectangle; }
        }

        public Int32 Left
        {
            get { return this.X; }
        }

        public Int32 Right
        {
            get { return (this.X + this.Width); }
        }

        public Int32 Top
        {
            get { return this.Y; }
        }

        public Int32 Bottom
        {
            get { return (this.Y + this.Height); }
        }

        #endregion Public Properties


        #region Constructors

        public Rectangle(Int32 x, Int32 y, Int32 width, Int32 height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        #endregion Constructors


        #region Public Methods

        public static Boolean operator ==(Rectangle a, Rectangle b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

		public Boolean Contains(Int32 x, Int32 y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }
		
        public Boolean Contains(Point value)
        {
            return ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        public Boolean Contains(Rectangle value)
        {
            return ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        public static Boolean operator !=(Rectangle a, Rectangle b)
        {
            return !(a == b);
        }

        public void Offset(Point offset)
        {
            X += offset.X;
            Y += offset.Y;
        }

        public void Offset(Int32 offsetX, Int32 offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }
		
		public Point Location
		{
			get 
			{
				return new Point(this.X, this.Y);
			}
			set
			{
				X = value.X;
				Y = value.Y;
			}
		}
		
		public Point Center
		{
			get 
			{
				// This is incorrect
				//return new Point( (this.X + this.Width) / 2,(this.Y + this.Height) / 2 );
				// What we want is the Center of the rectangle from the X and Y Origins
				return new Point(this.X + (this.Width / 2), this.Y + (this.Height / 2));
			}
		}




        public void Inflate(Int32 horizontalValue, Int32 verticalValue)
        {
            X -= horizontalValue;
            Y -= verticalValue;
            Width += horizontalValue * 2;
            Height += verticalValue * 2;
        }
		
		public Boolean IsEmpty
        {
            get
            {
                return ((((this.Width == 0) && (this.Height == 0)) && (this.X == 0)) && (this.Y == 0));
            }
        }

        public Boolean Equals(Rectangle other)
        {
            return this == other;
        }

        public override Boolean Equals(Object obj)
        {
            return (obj is Rectangle) ? this == ((Rectangle)obj) : false;
        }

        public override String ToString()
        {
            return String.Format("{{X:{0} Y:{1} Width:{2} Height:{3}}}", X, Y, Width, Height);
        }

        public override Int32 GetHashCode()
        {
            return (this.X ^ this.Y ^ this.Width ^ this.Height);
        }

        public Boolean Intersects(Rectangle value)
        {
            return value.Left < Right       && 
                   Left       < value.Right && 
                   value.Top  < Bottom      &&
                   Top        < value.Bottom;            
        }


        public void Intersects(ref Rectangle value, out Boolean result)
        {
            result = value.Left < Right       && 
                     Left       < value.Right && 
                     value.Top  < Bottom      &&
                     Top        < value.Bottom;
        }

        public static Rectangle Intersect(Rectangle value1, Rectangle value2)
        {
            Rectangle rectangle;
            Intersect(ref value1, ref value2, out rectangle);
            return rectangle;
        }


        public static void Intersect(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
        {
            if (value1.Intersects(value2))
            {
                Int32 right_side = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
                Int32 left_side = Math.Max(value1.X, value2.X);
                Int32 top_side = Math.Max(value1.Y, value2.Y);
                Int32 bottom_side = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);
                result = new Rectangle(left_side, top_side, right_side - left_side, bottom_side - top_side);
            }
            else
            {
                result = new Rectangle(0, 0, 0, 0);
            }
        }
		
		public static Rectangle Union(Rectangle value1, Rectangle value2)
		{
			Int32 x = Math.Min (value1.X, value2.X);
			Int32 y = Math.Min (value1.Y, value2.Y);
			return new Rectangle(x, y,
			                     Math.Max (value1.Right, value2.Right) - x,
				                     Math.Max (value1.Bottom, value2.Bottom) - y);
		}
		
		public static void Union(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
		{
			result.X = Math.Min (value1.X, value2.X);
			result.Y = Math.Min (value1.Y, value2.Y);
			result.Width = Math.Max (value1.Right, value2.Right) - result.X;
			result.Height = Math.Max (value1.Bottom, value2.Bottom) - result.Y;
		}
				
        #endregion Public Methods
    }
}
