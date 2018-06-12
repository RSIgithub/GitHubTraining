// ---------------------------------------------------------------------------------------------
// TrendObject.cs
// ---------------------------------------------------------------------------------------------
// Summary :
// ---------------------------------------------------------------------------------------------
// Frédéric POINDRON - 07/09/2016, 11:05
// ---------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Model
{
    using System;
    using System.Xml.Serialization;
    using RSI.Common.Mvvm;

    public abstract class TrendObject : SimpleViewModelDisposable, IEquatable<TrendObject>
    {
        public bool Equals(TrendObject other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return string.Equals(this._id, other._id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((TrendObject)obj);
        }

        public override int GetHashCode()
        {
            return this._id.GetHashCode();
        }

        public static bool operator ==(TrendObject left, TrendObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TrendObject left, TrendObject right)
        {
            return !Equals(left, right);
        }

        private readonly string _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrendObject"/> class.
        /// </summary>
        protected TrendObject()
        {
            this._id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        ///     The unique identifier.
        /// </value>
        [XmlIgnore]
        public string Id
        {
            get
            {
                return this._id;
            }
        }
    }
}