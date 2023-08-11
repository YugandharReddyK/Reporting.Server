using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class WaitObservatoryCorrectedSurvey // TODO: Suhail - Related to state  : IObjectWithState
    {
        public WaitObservatoryCorrectedSurvey()
        {
            //State = State.Added;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid CorrectedSurveyId { get; set; }

        public Guid ObservatoryStationId { get; set; }

        public Guid WellId { get; set; }

        public DateTime ReserveTime { get; set; }

        public DateTime ObservatoryTime { get; set; }

        [NotMapped]
        public MxSState State { get; protected set; }

        internal void SetState(MxSState state)
        {
            State = state;
        }

        //void IObjectWithState.SetState(MxSState state)
        //{
        //    SetState(state);
        //}

        public void ResetState()
        {
            SetState(MxSState.Unchanged);
        }

        public bool HasChanged()
        {
            return State != MxSState.Unchanged;
        }

        public void CasCadeDelete()
        {
            Delete();
        }

        public void UpdateState(MxSState state)
        {
            SetState(state);
        }

        public virtual void Delete()
        {
            SetState(MxSState.Deleted);
        }

        public bool Deleted { get; set; }

        public void UpdateObsTime(DateTime? reserveTime, DateTime? observatoryTime)
        {
            if (reserveTime.HasValue)
            {
                ReserveTime = reserveTime.Value;
                SetState(MxSState.Modified);
            }
            if (observatoryTime.HasValue)
            {
                ObservatoryTime = observatoryTime.Value;
                SetState(MxSState.Modified);
            }
        }
    }
}
