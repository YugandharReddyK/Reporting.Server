using Newtonsoft.Json;
using System.Runtime.Serialization;
using Sperry.MxS.Core.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class OdisseusToolCodeSection : DataModelBase
    {
        #region Private Members

        private double _endDepth;
        private OdisseusToolCodeParams _odisseusToolCodeParams;
        private string _toolCodeName;
        private Guid _odisseusToolCodeParamsId = Guid.Empty;
        private bool _showEnd;

        #endregion

        public OdisseusToolCodeSection()
        { }

        #region  Constructor
        public OdisseusToolCodeSection(OdisseusToolCodeSection odisseusToolCodeSectionToCopy) : this()
        {
            Customer = odisseusToolCodeSectionToCopy.Customer;
            StartDepth = odisseusToolCodeSectionToCopy.StartDepth;
            EndDepth = odisseusToolCodeSectionToCopy.EndDepth;
            OdisseusToolCodeParams = odisseusToolCodeSectionToCopy.OdisseusToolCodeParams;
            ToolCodeName = odisseusToolCodeSectionToCopy.ToolCodeName;
            OdisseusToolCodeParamsId = odisseusToolCodeSectionToCopy.OdisseusToolCodeParamsId;
        }
        #endregion

        #region Public Methods 

        [NotMapped]
        [JsonIgnore]
        public double PreviousWpEndDepth { get; set; }

        [JsonProperty]
        public Guid WellId { get; set; }

        [DataMember]
        public OdisseusToolCodeParams OdisseusToolCodeParams
        {
            get { return _odisseusToolCodeParams; }
            set
            {
                if (value != _odisseusToolCodeParams)
                {
                    _odisseusToolCodeParams = value;
                    if (_odisseusToolCodeParams != null)
                    {
                        _toolCodeName = _odisseusToolCodeParams.ToolCodeName;
                        _odisseusToolCodeParamsId = _odisseusToolCodeParams.Id;
                    }
                }
            }
        }


        [NotMapped]
        [JsonProperty]
        public bool IsStartDepthValid { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool IsEndDepthValid { get; set; }

        [NotMapped]
        [DataMember]
        public bool ShowEnd
        {
            get { return _showEnd; }
            set
            {
                if (_showEnd != value)
                {
                    _showEnd = value;
                    if (_showEnd)
                    {
                        EndDepth = MxSConstant.MaximumDepth;
                    }
                    else
                    {
                        if (PreviousWpEndDepth != 0)
                            EndDepth = PreviousWpEndDepth;
                    }
                    
                }
            }
        }

        [JsonProperty]
        public string ToolCodeName { get; set; }

        [JsonProperty]
        public Guid OdisseusToolCodeParamsId { get; set; }

        [JsonProperty]
        public Well Well { get; set; }

        [JsonProperty]
        public double StartDepth { get; set; }

        [JsonProperty]
        public double EndDepth
        {
            get { return _endDepth; }
            set
            {
                if (_endDepth != value)
                {
                    this.DecideBeforeSetEndDepth(value);
                    _endDepth = value;
                }
            }
        }


        [JsonProperty]
        public string Customer { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool IsEndDepthEnabled { get; set; }

        // TODO: Sandeep - Related To Satate

        //public override void ResetState()
        //{
        //    base.ResetState();
        //    if (OdisseusToolCodeParams != null)
        //    {
        //        OdisseusToolCodeParams.ResetState();
        //    }
        //}
        #endregion
    }
}
