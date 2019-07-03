using UnityEngine;

public struct Position {
	public sbyte X { get; }
	public sbyte Y { get; }
	public sbyte Z { get; }
	public sbyte[] Coordinates => new sbyte[] { X, Y, Z };
	public Vector3Int Vector => new Vector3Int(X, Y, Z);

	public sbyte this[sbyte axisNumber] => Coordinates[axisNumber];

	public Position(sbyte x, sbyte y, sbyte z) {
		X = x;
		Y = y;
		Z = z;
	}

	public Position(sbyte[] coordinates) {
		X = coordinates[0];
		Y = coordinates[1];
		Z = coordinates[2];
	}
}
