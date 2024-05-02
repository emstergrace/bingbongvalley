using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Objective
{

    public static string StringNotifier = "objective triggered";

    [SerializeField] private ObjectiveType type; public ObjectiveType Type { get { return type; } }
    [SerializeField] private string description; public string baseDescription { get { return description; } }
    [SerializeField] private string notifier; public string Notifier { get { return notifier; } }
    public string Description { get { return string.Format(description, Completed, currentAmount, requiredAmount); } } // Format is {0} for completed status, {1} for current amount, {2} for required amount
    
    public bool Completed { get; private set; } = false;
    [SerializeField] private bool isRequired = true; public bool IsRequired { get { return isRequired; } }

    [SerializeField] private int requiredAmount = 1; public int RequiredAmount { get { return requiredAmount; } }
    [SerializeField] private int currentAmount = 0; public int CurrentAmount { get { return currentAmount; } }

    public delegate void OnObjectiveComplete(Objective obj);
    public event OnObjectiveComplete OnCompletedCallback;

    public delegate void OnProgressChanged(int current, Objective obj);
    public event OnProgressChanged OnProgressChangedCallback;

    public Objective(ObjectiveType t, string d, string n, bool req, int reqAmt) {
        type = t;
        description = d;
        notifier = n;
        isRequired = req;
        requiredAmount = reqAmt;
	} // End of Objective() constructor.

    public Objective(Objective o) {
        type = o.type;
        description = o.baseDescription;
        notifier = o.Notifier;
        isRequired = o.IsRequired;
        requiredAmount = o.RequiredAmount;
	} // End of Objective() constructor.

    public void TryProgressing(string notif, int amount) {
        if (IsNotifierEventMatching(notif)) {
            ChangeProgress(amount);
		}
	} // End of TryProgressing().

    public bool IsNotifierEventMatching(string notif) {
        return notif.Equals(GetNotifier(type) + notifier);
	}

    public int ChangeProgress(int amt = 1) {
        if (!Completed) {
            return SetProgress(currentAmount + amt);
		}
        return currentAmount;
	} // End of IncrementAmount().

    public int SetProgress(int amt) {
        currentAmount = amt;
        OnProgressChangedCallback?.Invoke(currentAmount, this);

        if (IsProgressSufficientToComplete()) {
            Complete();
		}

        return currentAmount;
	} // End of SetProgress(0.

    public virtual bool IsProgressSufficientToComplete() {
        return (currentAmount >= requiredAmount);

    } // End of IsProgressSufficientToComplete().

    private void Complete() {
        Completed = true;
        OnCompletedCallback?.Invoke(this);
    } // End of Complete().

    public void Restart() {
        currentAmount = 0;
        Completed = false;
	} // End of Restart().

    public static string TravelNotifier = "visited ";
    public static string BountyNotifier = "killed ";
    public static string ItemNotifier = "picked up ";
    public static string DeliveryNotifier = "delivered ";
    public static string TalkNotifier = "talked to ";

    public static string GetNotifier(ObjectiveType type) {
        switch (type) {
            case ObjectiveType.Travel:
                return TravelNotifier;
            case ObjectiveType.Bounty:
                return BountyNotifier;
            case ObjectiveType.Item:
                return ItemNotifier;
            case ObjectiveType.Delivery:
                return DeliveryNotifier;
            case ObjectiveType.Talk:
                return TalkNotifier;
            default:
                return "";
		}
	} // End of GetNotifier().

} // End of Objective

[System.Serializable]
public enum ObjectiveType
{
    Travel, // Notifier is 'visited [place]'
    Bounty, // Notifier is 'killed [enemy name]'
    Item, // Notifier is 'picked up [item]'
    Delivery, // Notifier is 'delivered [item]'
    Talk, // Notifier is 'talked to [npc name]'
    Misc // Notifier is '[string notifier]'
} // End of ObjectiveType.