﻿namespace Dbscan;

/// <summary>
/// Contains static methods to run the DBSCAN algorithm.
/// </summary>
public static class Dbscan
{
	/// <summary>
	/// Run the DBSCAN algorithm on a collection of points, using the default index
	/// (<see cref="ListSpatialIndex{T}"/>).
	/// </summary>
	/// <typeparam name="T">The type of elements to cluster.</typeparam>
	/// <param name="data">The collection of elements to cluster.</param>
	/// <param name="epsilon">The epsilon parameter to use in the algorithm; used to determine the radius of the circle to find neighboring points.</param>
	/// <param name="minimumPointsPerCluster">The minimum number of points required to create a cluster or to add additional points to the cluster.</param>
	/// <returns>A <see cref="ClusterSet{T}"/> containing the list of <see cref="Cluster{T}"/>s and a list of unclustered points.</returns>
	/// <remarks>This method is an O(N^2) operation, where N is the Length of the dataset</remarks>
	public static ClusterSet<T> CalculateClusters<T>(
		IEnumerable<T> data,
		double epsilon,
		int minimumPointsPerCluster)
		where T : IPointData
	{
		var pointInfos = data
			.Select(p => new PointInfo<T>(p))
			.ToList();

		return CalculateClusters(
			new ListSpatialIndex<PointInfo<T>>(pointInfos),
			epsilon,
			minimumPointsPerCluster);
	}

	/// <summary>
	/// Run the DBSCAN algorithm on a collection of points, the specified pre-filled <see cref="ISpatialIndex{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of elements to cluster.</typeparam>
	/// <param name="index">The collection of elements to cluster.</param>
	/// <param name="epsilon">The epsilon parameter to use in the algorithm; used to determine the radius of the circle to find neighboring points.</param>
	/// <param name="minimumPointsPerCluster">The minimum number of points required to create a cluster or to add additional points to the cluster.</param>
	/// <returns>A <see cref="ClusterSet{T}"/> containing the list of <see cref="Cluster{T}"/>s and a list of unclustered points.</returns>
	public static ClusterSet<T> CalculateClusters<T>(
		ISpatialIndex<PointInfo<T>> index,
		double epsilon,
		int minimumPointsPerCluster)
		where T : IPointData
	{
		var points = index.Search().ToList();

		var clusters = new List<Cluster<T>>();

		foreach (var p in points)
		{
			var isVisited = p.ClusterType != ClusterType.Unknown;
			if (isVisited)
				continue;

			var candidates = index.Search(p, epsilon);

			if (candidates.Count >= minimumPointsPerCluster)
			{
				clusters.Add(
					BuildCluster(
						index,
						p,
						candidates,
						epsilon,
						minimumPointsPerCluster));
			}
			else
			{
				p.ClusterType = ClusterType.Noise;
			}
		}

		System.Diagnostics.Debug.Assert(!points.Any(p => p.ClusterType == ClusterType.Unknown));
		return new ClusterSet<T>
		{
			Clusters = clusters,
			UnclusteredObjects = points
				.Where(p => p.ClusterType == ClusterType.Noise)
				.Select(p => p.Item)
				.ToList(),
		};
	}

	private static Cluster<T> BuildCluster<T>(ISpatialIndex<PointInfo<T>> index, PointInfo<T> point, IReadOnlyList<PointInfo<T>> neighborhood, double epsilon, int minimumPointsPerCluster)
		where T : IPointData
	{
		var points = new List<T>() { point.Item };

		System.Diagnostics.Debug.Assert(neighborhood.Count >= minimumPointsPerCluster);
		point.ClusterType = ClusterType.ClusterCore;

		var queue = new Queue<PointInfo<T>>(neighborhood);
		while (queue.Any())
		{
			var newPoint = queue.Dequeue();
			var isClaimed = newPoint.ClusterType == ClusterType.ClusterBorder || newPoint.ClusterType == ClusterType.ClusterCore;
			if (isClaimed)
				continue;

			var newNeighbors = index.Search(newPoint, epsilon);
			if (newNeighbors.Count >= minimumPointsPerCluster)
			{
				newPoint.ClusterType = ClusterType.ClusterCore;
				foreach (var p in newNeighbors)
					queue.Enqueue(p);
			}
			else
			{
				newPoint.ClusterType = ClusterType.ClusterBorder;
			}
			points.Add(newPoint.Item);
		}

		return new Cluster<T> { Objects = points, };
	}

}
