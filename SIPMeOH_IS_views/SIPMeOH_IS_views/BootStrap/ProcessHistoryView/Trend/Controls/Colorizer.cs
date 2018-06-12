//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file=Colorizer.cs" company="RSI">
//    Copyright (c) RSI - All Rights Reserved
//  </copyright>
//  <summary>
//    
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public enum TrendColor
    {
        R0C0,
        R1C0,
        R2C0,
        R3C0,
        R0C1,
        R1C1,
        R2C1,
        R3C1,
        R0C2,
        R1C2,
        R2C2,
        R3C2,
        R0C3,
        R1C3,
        R2C3,
        R3C3,
    }

    public class Colorizer
    {
        #region Fields

        private ReferenceColorCollection _referenceColors;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Colorizer"/> class.
        /// </summary>
        /// <param name="colors">The colors.</param>
        public Colorizer(IEnumerable<string> colors)
        {
            this._referenceColors = new ReferenceColorCollection(colors);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this._referenceColors = new ReferenceColorCollection(DefaultColors);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Colorizer"/> class.
        /// </summary>
        public Colorizer()
            : this(DefaultColors)
        {
        }

        #endregion

        #region Properties


        public static string EnumToString(TrendColor color)
        {
            string result;
            return EnumsToString.TryGetValue(color, out result) ? result : "Black";
        }

        public static TrendColor StringToTrendColor(string color)
        {
            foreach (var kvp in EnumsToString)
            {
                if (string.Compare(color, kvp.Value, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return kvp.Key;
                }
            }

            return TrendColor.R0C0;
        }

        public static readonly Dictionary<TrendColor, string> EnumsToString = new Dictionary<TrendColor, string>
        {
            {TrendColor.R0C0, "Black"},
            {TrendColor.R1C0, "Navy"},
            {TrendColor.R2C0, "Maroon"},
            {TrendColor.R3C0, "#FF005500"},
            {TrendColor.R0C1, "#FF4B4B4B"},
            {TrendColor.R1C1, "#FF8000FF"},
            {TrendColor.R2C1, "Magenta"},
            {TrendColor.R3C1, "#FF009600"},
            {TrendColor.R0C2, "Gray"},
            {TrendColor.R1C2, "Blue"},
            {TrendColor.R2C2, "Red"},
            {TrendColor.R3C2, "Lime"},
            {TrendColor.R0C3, "White"},
            {TrendColor.R1C3, "Cyan"},
            {TrendColor.R2C3, "#FFFF8000"},
            {TrendColor.R3C3, "Yellow"},
        };

        /// <summary>
        /// Gets the default colors.
        /// </summary>
        /// <value>
        /// The default colors.
        /// </value>
        private static IEnumerable<string> DefaultColors
        {
            get
            {
                return EnumsToString.Values;
            }
        }

        #endregion

        #region Public Methods and Operators

        public string Next()
        {
            var minimum = this._referenceColors.Minimum;
            minimum.Increment();
            return minimum.Color;
        }

        public void Release(string color)
        {
            var referenceColor = this._referenceColors.Find(color);
            if (referenceColor == null)
            {
                return;
            }
            referenceColor.Decrement();
        }

        public void Use(string color)
        {
            var referenceColor = this._referenceColors.Find(color);
            if (referenceColor == null)
            {
                return;
            }
            referenceColor.Increment();
        }

        #endregion

        private class ReferenceColor
        {
            #region Static Fields

            public static readonly ReferenceColor Maximum = new ReferenceColor(string.Empty, int.MaxValue);

            #endregion

            #region Fields

            private readonly string _color;

            #endregion

            #region Constructors and Destructors

            static ReferenceColor()
            {
            }

            public ReferenceColor(string color)
                : this(color, 0)
            {
            }

            private ReferenceColor(string color, int referenceCount)
            {
                this._color = color;
                this.Count = referenceCount;
            }

            #endregion

            #region Public Properties

            public string Color
            {
                get
                {
                    return this._color;
                }
            }

            /// <summary>
            ///     Gets or sets the count.
            /// </summary>
            /// <value>
            ///     The count.
            /// </value>
            public int Count { get; private set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///     Decrements this instance.
            /// </summary>
            public void Decrement()
            {
                this.Count = Math.Max(0, this.Count - 1);
            }

            /// <summary>
            ///     Increments this instance.
            /// </summary>
            public void Increment()
            {
                this.Count = Math.Min(this.Count + 1, int.MaxValue);
            }

            public override string ToString()
            {
                return string.Format("Color = {0}, Count = {1}", this._color, this.Count);
            }

            #endregion
        }

        private sealed class ReferenceColorCollection : Collection<ReferenceColor>
        {
            #region Constructors and Destructors

            public ReferenceColorCollection(IEnumerable<string> colors)
            {
                if (colors == null)
                {
                    throw new ArgumentNullException();
                }
                foreach (var color in colors)
                {
                    this.Add(new ReferenceColor(color));
                }
            }

            #endregion

            #region Public Properties

            public ReferenceColor Minimum
            {
                get
                {
                    ReferenceColor referenceColor1 = ReferenceColor.Maximum;
                    foreach (var referenceColor2 in this)
                    {
                        if (referenceColor1.Count > referenceColor2.Count)
                        {
                            referenceColor1 = referenceColor2;
                        }
                    }
                    return referenceColor1;
                }
            }

            #endregion

            #region Public Methods and Operators

            public ReferenceColor Find(string color)
            {
                foreach (var referenceColor in this)
                {
                    if (referenceColor.Color == color)
                    {
                        return referenceColor;
                    }
                }
                return null;
            }

            #endregion
        }
    }
}