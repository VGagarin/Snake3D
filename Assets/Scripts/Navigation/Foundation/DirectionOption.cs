public struct DirectionOption {
	public sbyte Direction { get; }
	public sbyte AxisNumber { get; }

	public DirectionOption(sbyte direction, sbyte axisNumber) {
		Direction = direction;
		AxisNumber = axisNumber;
	}

	public static DirectionOption IdentifyDirectionOption(Position start, Position end) {
		foreach (sbyte axisNumber in new sbyte[] { 0, 1, 2 })
			if (start[axisNumber] != end[axisNumber]) {
				sbyte direction = (sbyte)(end[axisNumber] - start[axisNumber]);
				return new DirectionOption(direction, axisNumber);
			}
		return default;
	}
}