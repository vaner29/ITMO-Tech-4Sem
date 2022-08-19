using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.FSharp.Core;

[assembly: FSharpInterfaceDataVersion(2, 0, 0)]
[assembly: AssemblyVersion("0.0.0.0")]
[CompilationMapping(SourceConstructFlags.Module)]
public static class FarshLibary
{
    [Serializable]
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [CompilationMapping(SourceConstructFlags.SumType)]
    public abstract class Shape : IEquatable<Shape>, IStructuralEquatable, IComparable<Shape>, IComparable, IStructuralComparable
    {
        public static class Tags
        {
            public const int Circle = 0;

            public const int EquilateralTriangle = 1;

            public const int Square = 2;

            public const int Rectangle = 3;
        }

        [Serializable]
        [SpecialName]
        [DebuggerTypeProxy(typeof(Circle@DebugTypeProxy))]
        [DebuggerDisplay("{__DebugDisplay(),nq}")]
        public class Circle : Shape
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal readonly double item;

            [CompilationMapping(SourceConstructFlags.Field, 0, 0)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            public double Item
            {
                [CompilerGenerated]
                [DebuggerNonUserCode]
                get
                {
                    return item;
                }
            }

            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal Circle(double item)
                : base(0)
            {
                this.item = item;
            }
        }

        [Serializable]
        [SpecialName]
        [DebuggerTypeProxy(typeof(EquilateralTriangle@DebugTypeProxy))]
        [DebuggerDisplay("{__DebugDisplay(),nq}")]
        public class EquilateralTriangle : Shape
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal readonly double item;

            [CompilationMapping(SourceConstructFlags.Field, 1, 0)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            public double Item
            {
                [CompilerGenerated]
                [DebuggerNonUserCode]
                get
                {
                    return item;
                }
            }

            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal EquilateralTriangle(double item)
                : base(1)
            {
                this.item = item;
            }
        }

        [Serializable]
        [SpecialName]
        [DebuggerTypeProxy(typeof(Square@DebugTypeProxy))]
        [DebuggerDisplay("{__DebugDisplay(),nq}")]
        public class Square : Shape
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal readonly double item;

            [CompilationMapping(SourceConstructFlags.Field, 2, 0)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            public double Item
            {
                [CompilerGenerated]
                [DebuggerNonUserCode]
                get
                {
                    return item;
                }
            }

            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal Square(double item)
                : base(2)
            {
                this.item = item;
            }
        }

        [Serializable]
        [SpecialName]
        [DebuggerTypeProxy(typeof(Rectangle@DebugTypeProxy))]
        [DebuggerDisplay("{__DebugDisplay(),nq}")]
        public class Rectangle : Shape
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal readonly double item1;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal readonly double item2;

            [CompilationMapping(SourceConstructFlags.Field, 3, 0)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            public double Item1
            {
                [CompilerGenerated]
                [DebuggerNonUserCode]
                get
                {
                    return item1;
                }
            }

            [CompilationMapping(SourceConstructFlags.Field, 3, 1)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            public double Item2
            {
                [CompilerGenerated]
                [DebuggerNonUserCode]
                get
                {
                    return item2;
                }
            }

            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal Rectangle(double item1, double item2)
                : base(3)
            {
                this.item1 = item1;
                this.item2 = item2;
            }
        }

        [SpecialName]
        internal class Circle@DebugTypeProxy
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal Circle _obj;

            [CompilationMapping(SourceConstructFlags.Field, 0, 0)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            public double Item
            {
                [CompilerGenerated]
                [DebuggerNonUserCode]
                get
                {
                    return _obj.item;
                }
            }

            [CompilerGenerated]
            [DebuggerNonUserCode]
            public Circle@DebugTypeProxy(Circle obj)
            {
                _obj = obj;
            }
        }

        [SpecialName]
        internal class EquilateralTriangle@DebugTypeProxy
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal EquilateralTriangle _obj;

            [CompilationMapping(SourceConstructFlags.Field, 1, 0)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            public double Item
            {
                [CompilerGenerated]
                [DebuggerNonUserCode]
                get
                {
                    return _obj.item;
                }
            }

            [CompilerGenerated]
            [DebuggerNonUserCode]
            public EquilateralTriangle@DebugTypeProxy(EquilateralTriangle obj)
            {
                _obj = obj;
            }
        }

        [SpecialName]
        internal class Square@DebugTypeProxy
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal Square _obj;

            [CompilationMapping(SourceConstructFlags.Field, 2, 0)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            public double Item
            {
                [CompilerGenerated]
                [DebuggerNonUserCode]
                get
                {
                    return _obj.item;
                }
            }

            [CompilerGenerated]
            [DebuggerNonUserCode]
            public Square@DebugTypeProxy(Square obj)
            {
                _obj = obj;
            }
        }

        [SpecialName]
        internal class Rectangle@DebugTypeProxy
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            internal Rectangle _obj;

            [CompilationMapping(SourceConstructFlags.Field, 3, 0)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            public double Item1
            {
                [CompilerGenerated]
                [DebuggerNonUserCode]
                get
                {
                    return _obj.item1;
                }
            }

            [CompilationMapping(SourceConstructFlags.Field, 3, 1)]
            [CompilerGenerated]
            [DebuggerNonUserCode]
            public double Item2
            {
                [CompilerGenerated]
                [DebuggerNonUserCode]
                get
                {
                    return _obj.item2;
                }
            }

            [CompilerGenerated]
            [DebuggerNonUserCode]
            public Rectangle@DebugTypeProxy(Rectangle obj)
            {
                _obj = obj;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [CompilerGenerated]
        [DebuggerNonUserCode]
        internal readonly int _tag;

        [CompilerGenerated]
        [DebuggerNonUserCode]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Tag
        {
            [CompilerGenerated]
            [DebuggerNonUserCode]
            get
            {
                return _tag;
            }
        }

        [CompilerGenerated]
        [DebuggerNonUserCode]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsCircle
        {
            [CompilerGenerated]
            [DebuggerNonUserCode]
            get
            {
                return Tag == 0;
            }
        }

        [CompilerGenerated]
        [DebuggerNonUserCode]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsEquilateralTriangle
        {
            [CompilerGenerated]
            [DebuggerNonUserCode]
            get
            {
                return Tag == 1;
            }
        }

        [CompilerGenerated]
        [DebuggerNonUserCode]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsSquare
        {
            [CompilerGenerated]
            [DebuggerNonUserCode]
            get
            {
                return Tag == 2;
            }
        }

        [CompilerGenerated]
        [DebuggerNonUserCode]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsRectangle
        {
            [CompilerGenerated]
            [DebuggerNonUserCode]
            get
            {
                return Tag == 3;
            }
        }

        [CompilerGenerated]
        [DebuggerNonUserCode]
        internal Shape(int _tag)
        {
            this._tag = _tag;
        }

        [CompilationMapping(SourceConstructFlags.UnionCase, 0)]
        public static Shape NewCircle(double item)
        {
            return new Circle(item);
        }

        [CompilationMapping(SourceConstructFlags.UnionCase, 1)]
        public static Shape NewEquilateralTriangle(double item)
        {
            return new EquilateralTriangle(item);
        }

        [CompilationMapping(SourceConstructFlags.UnionCase, 2)]
        public static Shape NewSquare(double item)
        {
            return new Square(item);
        }

        [CompilationMapping(SourceConstructFlags.UnionCase, 3)]
        public static Shape NewRectangle(double item1, double item2)
        {
            return new Rectangle(item1, item2);
        }

        [SpecialName]
        [CompilerGenerated]
        [DebuggerNonUserCode]
        internal object __DebugDisplay()
        {
            return ExtraTopLevelOperators.PrintFormatToString(new PrintfFormat<FSharpFunc<Shape, string>, Unit, string, string, string>("%+0.8A")).Invoke(this);
        }

        [CompilerGenerated]
        public override string ToString()
        {
            return ExtraTopLevelOperators.PrintFormatToString(new PrintfFormat<FSharpFunc<Shape, string>, Unit, string, string, Shape>("%+A")).Invoke(this);
        }

        [CompilerGenerated]
        public sealed override int CompareTo(Shape obj)
        {
            if (this != null)
            {
                if (obj != null)
                {
                    int tag = _tag;
                    int tag2 = obj._tag;
                    if (tag == tag2)
                    {
                        return CompareTo$cont@3(this, obj, null);
                    }
                    return tag - tag2;
                }
                return 1;
            }
            if (obj != null)
            {
                return -1;
            }
            return 0;
        }

        [CompilerGenerated]
        public sealed override int CompareTo(object obj)
        {
            return CompareTo((Shape)obj);
        }

        [CompilerGenerated]
        public sealed override int CompareTo(object obj, IComparer comp)
        {
            Shape shape = (Shape)obj;
            if (this != null)
            {
                if ((Shape)obj != null)
                {
                    int tag = _tag;
                    int tag2 = shape._tag;
                    if (tag == tag2)
                    {
                        return CompareTo$cont@3-1(comp, this, shape, null);
                    }
                    return tag - tag2;
                }
                return 1;
            }
            if ((Shape)obj != null)
            {
                return -1;
            }
            return 0;
        }

        [CompilerGenerated]
        public sealed override int GetHashCode(IEqualityComparer comp)
        {
            if (this != null)
            {
                int num = 0;
                switch (Tag)
                {
                    default:
                    {
                        Circle circle = (Circle)this;
                        num = 0;
                        return -1640531527 + (LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic(comp, circle.item) + ((num << 6) + (num >> 2)));
                    }
                    case 1:
                    {
                        EquilateralTriangle equilateralTriangle = (EquilateralTriangle)this;
                        num = 1;
                        return -1640531527 + (LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic(comp, equilateralTriangle.item) + ((num << 6) + (num >> 2)));
                    }
                    case 2:
                    {
                        Square square = (Square)this;
                        num = 2;
                        return -1640531527 + (LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic(comp, square.item) + ((num << 6) + (num >> 2)));
                    }
                    case 3:
                    {
                        Rectangle rectangle = (Rectangle)this;
                        num = 3;
                        num = -1640531527 + (LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic(comp, rectangle.item2) + ((num << 6) + (num >> 2)));
                        return -1640531527 + (LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic(comp, rectangle.item1) + ((num << 6) + (num >> 2)));
                    }
                }
            }
            return 0;
        }

        [CompilerGenerated]
        public sealed override int GetHashCode()
        {
            return GetHashCode(LanguagePrimitives.GenericEqualityComparer);
        }

        [CompilerGenerated]
        public sealed override bool Equals(object obj, IEqualityComparer comp)
        {
            if (this != null)
            {
                Shape shape = obj as Shape;
                if (shape != null)
                {
                    int tag = _tag;
                    int tag2 = shape._tag;
                    if (tag == tag2)
                    {
                        switch (Tag)
                        {
                            default:
                            {
                                Circle circle = (Circle)this;
                                Circle circle2 = (Circle)shape;
                                return circle.item == circle2.item;
                            }
                            case 1:
                            {
                                EquilateralTriangle equilateralTriangle = (EquilateralTriangle)this;
                                EquilateralTriangle equilateralTriangle2 = (EquilateralTriangle)shape;
                                return equilateralTriangle.item == equilateralTriangle2.item;
                            }
                            case 2:
                            {
                                Square square = (Square)this;
                                Square square2 = (Square)shape;
                                return square.item == square2.item;
                            }
                            case 3:
                            {
                                Rectangle rectangle = (Rectangle)this;
                                Rectangle rectangle2 = (Rectangle)shape;
                                if (rectangle.item1 == rectangle2.item1)
                                {
                                    return rectangle.item2 == rectangle2.item2;
                                }
                                return false;
                            }
                        }
                    }
                    return false;
                }
                return false;
            }
            return obj == null;
        }

        [CompilerGenerated]
        public sealed override bool Equals(Shape obj)
        {
            if (this != null)
            {
                if (obj != null)
                {
                    int tag = _tag;
                    int tag2 = obj._tag;
                    if (tag == tag2)
                    {
                        return Equals$cont@3(this, obj, null);
                    }
                    return false;
                }
                return false;
            }
            return obj == null;
        }

        [CompilerGenerated]
        public sealed override bool Equals(object obj)
        {
            Shape shape = obj as Shape;
            if (shape != null)
            {
                return Equals(shape);
            }
            return false;
        }
    }

    public static double pi
    {
        [CompilerGenerated]
        [DebuggerNonUserCode]
        get
        {
            return 3.141592654;
        }
    }

    [CompilerGenerated]
    internal static int CompareTo$cont@3(Shape @this, Shape obj, Unit unitVar)
    {
        switch (@this.Tag)
        {
            default:
            {
                Shape.Circle circle = (Shape.Circle)@this;
                Shape.Circle circle2 = (Shape.Circle)obj;
                IComparer genericComparer = LanguagePrimitives.GenericComparer;
                double item = circle.item;
                double item2 = circle2.item;
                if (item < item2)
                {
                    return -1;
                }
                if (item > item2)
                {
                    return 1;
                }
                if (item == item2)
                {
                    return 0;
                }
                return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic(genericComparer, item, item2);
            }
            case 1:
            {
                Shape.EquilateralTriangle equilateralTriangle = (Shape.EquilateralTriangle)@this;
                Shape.EquilateralTriangle equilateralTriangle2 = (Shape.EquilateralTriangle)obj;
                IComparer genericComparer = LanguagePrimitives.GenericComparer;
                double item = equilateralTriangle.item;
                double item2 = equilateralTriangle2.item;
                if (item < item2)
                {
                    return -1;
                }
                if (item > item2)
                {
                    return 1;
                }
                if (item == item2)
                {
                    return 0;
                }
                return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic(genericComparer, item, item2);
            }
            case 2:
            {
                Shape.Square square = (Shape.Square)@this;
                Shape.Square square2 = (Shape.Square)obj;
                IComparer genericComparer = LanguagePrimitives.GenericComparer;
                double item = square.item;
                double item2 = square2.item;
                if (item < item2)
                {
                    return -1;
                }
                if (item > item2)
                {
                    return 1;
                }
                if (item == item2)
                {
                    return 0;
                }
                return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic(genericComparer, item, item2);
            }
            case 3:
            {
                Shape.Rectangle rectangle = (Shape.Rectangle)@this;
                Shape.Rectangle rectangle2 = (Shape.Rectangle)obj;
                IComparer genericComparer = LanguagePrimitives.GenericComparer;
                double item = rectangle.item1;
                double item2 = rectangle2.item1;
                int num = ((item < item2) ? (-1) : ((item > item2) ? 1 : ((item != item2) ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic(genericComparer, item, item2) : 0)));
                if (num < 0)
                {
                    return num;
                }
                if (num > 0)
                {
                    return num;
                }
                genericComparer = LanguagePrimitives.GenericComparer;
                item = rectangle.item2;
                item2 = rectangle2.item2;
                if (item < item2)
                {
                    return -1;
                }
                if (item > item2)
                {
                    return 1;
                }
                if (item == item2)
                {
                    return 0;
                }
                return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic(genericComparer, item, item2);
            }
        }
    }

    [CompilerGenerated]
    internal static int CompareTo$cont@3-1(IComparer comp, Shape @this, Shape objTemp, Unit unitVar)
    {
        switch (@this.Tag)
        {
            default:
            {
                Shape.Circle circle = (Shape.Circle)@this;
                Shape.Circle circle2 = (Shape.Circle)objTemp;
                double item = circle.item;
                double item2 = circle2.item;
                if (item < item2)
                {
                    return -1;
                }
                if (item > item2)
                {
                    return 1;
                }
                if (item == item2)
                {
                    return 0;
                }
                return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic(comp, item, item2);
            }
            case 1:
            {
                Shape.EquilateralTriangle equilateralTriangle = (Shape.EquilateralTriangle)@this;
                Shape.EquilateralTriangle equilateralTriangle2 = (Shape.EquilateralTriangle)objTemp;
                double item = equilateralTriangle.item;
                double item2 = equilateralTriangle2.item;
                if (item < item2)
                {
                    return -1;
                }
                if (item > item2)
                {
                    return 1;
                }
                if (item == item2)
                {
                    return 0;
                }
                return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic(comp, item, item2);
            }
            case 2:
            {
                Shape.Square square = (Shape.Square)@this;
                Shape.Square square2 = (Shape.Square)objTemp;
                double item = square.item;
                double item2 = square2.item;
                if (item < item2)
                {
                    return -1;
                }
                if (item > item2)
                {
                    return 1;
                }
                if (item == item2)
                {
                    return 0;
                }
                return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic(comp, item, item2);
            }
            case 3:
            {
                Shape.Rectangle rectangle = (Shape.Rectangle)@this;
                Shape.Rectangle rectangle2 = (Shape.Rectangle)objTemp;
                double item = rectangle.item1;
                double item2 = rectangle2.item1;
                int num = ((item < item2) ? (-1) : ((item > item2) ? 1 : ((item != item2) ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic(comp, item, item2) : 0)));
                if (num < 0)
                {
                    return num;
                }
                if (num > 0)
                {
                    return num;
                }
                item = rectangle.item2;
                item2 = rectangle2.item2;
                if (item < item2)
                {
                    return -1;
                }
                if (item > item2)
                {
                    return 1;
                }
                if (item == item2)
                {
                    return 0;
                }
                return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic(comp, item, item2);
            }
        }
    }

    [CompilerGenerated]
    internal static bool Equals$cont@3(Shape @this, Shape obj, Unit unitVar)
    {
        switch (@this.Tag)
        {
            default:
            {
                Shape.Circle circle = (Shape.Circle)@this;
                Shape.Circle circle2 = (Shape.Circle)obj;
                double item = circle.item;
                double item2 = circle2.item;
                if (item == item2)
                {
                    return true;
                }
                if (item != item)
                {
                    return item2 != item2;
                }
                return false;
            }
            case 1:
            {
                Shape.EquilateralTriangle equilateralTriangle = (Shape.EquilateralTriangle)@this;
                Shape.EquilateralTriangle equilateralTriangle2 = (Shape.EquilateralTriangle)obj;
                double item = equilateralTriangle.item;
                double item2 = equilateralTriangle2.item;
                if (item == item2)
                {
                    return true;
                }
                if (item != item)
                {
                    return item2 != item2;
                }
                return false;
            }
            case 2:
            {
                Shape.Square square = (Shape.Square)@this;
                Shape.Square square2 = (Shape.Square)obj;
                double item = square.item;
                double item2 = square2.item;
                if (item == item2)
                {
                    return true;
                }
                if (item != item)
                {
                    return item2 != item2;
                }
                return false;
            }
            case 3:
            {
                Shape.Rectangle rectangle = (Shape.Rectangle)@this;
                Shape.Rectangle rectangle2 = (Shape.Rectangle)obj;
                double item = rectangle.item1;
                double item2 = rectangle2.item1;
                if (item == item2 || (item != item && item2 != item2))
                {
                    item = rectangle.item2;
                    item2 = rectangle2.item2;
                    if (item == item2)
                    {
                        return true;
                    }
                    if (item != item)
                    {
                        return item2 != item2;
                    }
                    return false;
                }
                return false;
            }
        }
    }

    public static double area(Shape myShape)
    {
        switch (myShape.Tag)
        {
            default:
            {
                Shape.Circle circle = (Shape.Circle)myShape;
                double item = circle.item;
                return 3.141592654 * item * item;
            }
            case 1:
            {
                Shape.EquilateralTriangle equilateralTriangle = (Shape.EquilateralTriangle)myShape;
                double item = equilateralTriangle.item;
                return Math.Sqrt(3.0) / 4.0 * item * item;
            }
            case 2:
            {
                Shape.Square square = (Shape.Square)myShape;
                double item = square.item;
                return item * item;
            }
            case 3:
            {
                Shape.Rectangle rectangle = (Shape.Rectangle)myShape;
                return rectangle.item1 * rectangle.item2;
            }
        }
    }
}
namespace <StartupCode$_>
{
    internal static class $FarshLibary
    {
    }
}
