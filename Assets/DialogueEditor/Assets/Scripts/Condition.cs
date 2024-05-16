namespace DialogueEditor
{
    public abstract class Condition
    {
        public enum eConditionType
        {
            IntCondition,
            BoolCondition,
            QuestCondition
        }

        public abstract eConditionType ConditionType { get; }

        public string ParameterName;
    }

    public class QuestCondition : Condition
	{
        public enum eCheckType
		{

            Inactive = 0,
            Active = 1,
            FinishedUncompleted = 2,
            Completed = 3,
            Failed = 4
        }

		public override eConditionType ConditionType { get { return eConditionType.QuestCondition; } }

        public eCheckType CheckType;
        public QuestStatus RequiredStatus;
	}

    public class IntCondition : Condition
    {
        public enum eCheckType
        {
            equal,
            lessThan,
            greaterThan
        }

        public override eConditionType ConditionType { get { return eConditionType.IntCondition; } }

        public eCheckType CheckType;
        public int RequiredValue;
    }

    public class BoolCondition : Condition
    {
        public enum eCheckType
        {
            equal,
            notEqual
        }

        public override eConditionType ConditionType { get { return eConditionType.BoolCondition; } }

        public eCheckType CheckType;
        public bool RequiredValue;
    }
}
