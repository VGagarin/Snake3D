using System.Collections.Generic;
using System.Linq;

public static class MapHelper {
	private static readonly byte mapSize;
	private static readonly IEnumerable<DirectionOption> directionOptions;

	static MapHelper() {
		mapSize = Map.MapSize;
		directionOptions = DirectionOptions();
	}

	public static IEnumerable<Position> NeighborPositions(Position pos) {
		foreach (DirectionOption dO in directionOptions) {
			sbyte[] coords = pos.Coordinates;
			coords[dO.AxisNumber] += dO.Direction;
			if (coords[dO.AxisNumber] >= 0 && coords[dO.AxisNumber] < mapSize)
				yield return new Position(coords);
		}
	}

	private static IEnumerable<DirectionOption> DirectionOptions() {
		sbyte[] axisNumbers = new sbyte[] { 0, 1, 2 };
		sbyte[] directions = new sbyte[] { -1, 1 };

		IEnumerable<DirectionOption> directionOptions = from axisNumber in axisNumbers
														from direction in directions
														select new DirectionOption(direction, axisNumber);
		return directionOptions;
	}

	public static Position GetNextPosition(Position pos, DirectionOption dO) {
		sbyte[] coords = pos.Coordinates;
		coords[dO.AxisNumber] += dO.Direction;
		return new Position(coords);
	}

	public static IEnumerable<Position> AllPositions() {
		for (sbyte x = 0; x < mapSize; x++)
			for (sbyte y = 0; y < mapSize; y++)
				for (sbyte z = 0; z < mapSize; z++)
					yield return new Position(x, y, z);
	}
}
