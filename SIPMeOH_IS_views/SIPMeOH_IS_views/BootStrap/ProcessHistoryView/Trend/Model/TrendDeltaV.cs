// ---------------------------------------------------------------------------------------------
// TrendDeltaV.cs
// ---------------------------------------------------------------------------------------------
// Summary :
// ---------------------------------------------------------------------------------------------
// Frédéric POINDRON - 12/09/2016, 09:30
// ---------------------------------------------------------------------------------------------

namespace SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Model
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using SIPMeOH_IS_views.BootStrap.ProcessHistoryView.Trend.Controls;
    using RSI.Common.Mvvm;
    using RSI.Common.Tools.Helpers;
    using RSI.Common.Tools.Serialization;
    using RSI.IndissPlus.Solution;

    [Serializable]
    [XmlRoot("Trend")]
    public class TrendDeltaV : TrendObject
    {
        private string _name;

        private PenCollection _pens;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrendDeltaV" /> class.
        /// </summary>
        public TrendDeltaV()
        {
            this._pens = new PenCollection();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrendDeltaV" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TrendDeltaV(string name) : this()
        {
            this._name = name;
        }

        /// <summary>
        ///     Gets the folder.
        /// </summary>
        /// <value>
        ///     The folder.
        /// </value>
        public static string Folder
        {
            get
            {
                var result = Path.Combine(ServiceContext.KernelService.WorkingDirectory, "Trends");
                result.CreateDirectory();
                return result;
            }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [XmlAttribute]
        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                this.Set(ref this._name, value);
            }
        }

        /// <summary>
        ///     Gets or sets the pens.
        /// </summary>
        /// <value>
        ///     The pens.
        /// </value>
        [XmlArrayItem("Pen")]
        public PenCollection Pens
        {
            get
            {
                return this._pens;
            }

            set
            {
                this.Set(ref this._pens, value);
            }
        }

        /// <summary>
        ///     Tries the load.
        /// </summary>
        /// <param name="pathFileName">Name of the path file.</param>
        /// <param name="trend">The trend.</param>
        /// <returns></returns>
        public static bool TryLoad(string pathFileName, out TrendDeltaV trend)
        {
            // Check file name
            if (!File.Exists(pathFileName))
            {
                trend = null;
                return false;
            }

            // Load
            try
            {
                trend = ObjectXmlSerializer<TrendDeltaV>.Load(typeof(TrendDeltaV), pathFileName);
                trend.SourcePathFilename = pathFileName;
            }
            catch (Exception e)
            {
                ServiceContext.MessageDisplayService.ShowError("Load failed", e);
                trend = null;
            }

            return trend != null;
        }

        /// <summary>
        ///     Adds the pen.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        public void AddPen(string parameterName)
        {
            this._pens.Add(new PenDeltaV(parameterName, this.Colorizer.Next()));
        }

        /// <summary>
        /// Gets the serialized string.
        /// </summary>
        /// <value>
        /// The serialized string.
        /// </value>
        public string SerializedString
        {
            get
            {
                return ObjectXmlSerializer<TrendDeltaV>.Save(this);
            }
        }

        /// <summary>
        ///     Tries the save.
        /// </summary>
        /// <returns></returns>
        public bool TrySave()
        {
            // Get target path file 
            var targetPathFilename = this.PathFileNameOf(this._name);

            // Check validity
            if (Path.GetFileName(targetPathFilename).IsValidFileName() == false)
            {
                ServiceContext2.MessageDisplayService.ShowError("Saving Trend",
                    $"Invalid fileName : {Path.GetFileName(targetPathFilename)}");
                return false;
            }

            if (!File.Exists(this.SourcePathFilename))
            {
                this.SourcePathFilename = null;
            }

            // Check if not already exist
            if (this.IsNew || (this.SourcePathFilename != null && targetPathFilename != this.SourcePathFilename))
            {
                if (File.Exists(targetPathFilename))
                {
                    if (
                        ServiceContext.MessageDisplayService.Question("Saving Trend",
                            $"A trend named {this._name} already exist. Do you want to overwrite it ?") == false)
                    {
                        return false;
                    }
                }
            }

            // Have to manage previous ?
            if (this.SourcePathFilename != null && (targetPathFilename != this.SourcePathFilename))
            {
                try
                {
                    File.Delete(this.SourcePathFilename);
                }
                catch (Exception e)
                {
                    ServiceContext.MessageDisplayService.ShowError("Deleting file", e);
                }
            }

            // Save
            try
            {
                ObjectXmlSerializer<TrendDeltaV>.Save(this, targetPathFilename);

                // No more new
                this._isNew = false;

                // Keep previous
                this.SourcePathFilename = targetPathFilename;
                return true;
            }
            catch (Exception e)
            {
                ServiceContext2.MessageDisplayService.ShowError("Saving Trend", e);
                return false;
            }
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                foreach (var pen in this._pens)
                {
                    pen.Dispose();
                }
            }
        }

        /// <summary>
        /// Pathes the file name of.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private string PathFileNameOf(string name)
        {
            return Path.Combine(Folder, $"{name.Replace(" ", "_")}.xml");
        }

        #region Properties

        private Colorizer _lazyColorizer;
        private bool _isNew;

        /// <summary>
        /// Gets the source path filename.
        /// </summary>
        /// <value>
        /// The source path filename.
        /// </value>
        [XmlIgnore]
        public string SourcePathFilename
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the colorizer.
        /// </summary>
        /// <value>
        ///     The colorizer.
        /// </value>
        private Colorizer Colorizer
        {
            get
            {
                if (this._lazyColorizer != null)
                {
                    return this._lazyColorizer;
                }

                this._lazyColorizer = new Colorizer();
                foreach (var pen in this.Pens)
                {
                    this._lazyColorizer.Use(pen.Color);
                }

                return this._lazyColorizer;
            }
        }

        /// <summary>
        ///     Gets the name of the path file.
        /// </summary>
        /// <value>
        ///     The name of the path file.
        /// </value>
        public string PathFileName
        {
            get
            {
                return this.PathFileNameOf(this._name);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is new.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </value>
        public bool IsNew => this._isNew;

        public void SetNew()
        {
            this._isNew = true;
        }

        #endregion
    }
}