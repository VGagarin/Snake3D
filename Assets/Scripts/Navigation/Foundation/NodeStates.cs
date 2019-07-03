using System;

[Flags]
public enum NodeStates : sbyte {
	None = 0,
	Free = 1,
	OccupiedByItem = 2,
	Busy = 4,
	PotentialBusy = 8,
	InAccess = Free | OccupiedByItem
}
