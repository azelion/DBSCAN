namespace Dbscan;

/// <summary>
/// Type how a point is clustered. Default value = <see cref="Unknown"/>
/// </summary>
public enum ClusterType
{
	/// <summary>
	/// Point is not visited yet for clustering.
	/// </summary>
	Unknown = 0,
	/// <summary>
	/// Point is not added to any cluster, it does not meet the criteria to be added to a cluster (epsilon and minimumPointsPerCluster)
	/// </summary>
	Noise,
	/// <summary>
	/// Point is added to a cluster, but not the core, means it satisfies the epsilon, but not the minimumPointsPerCluster
	/// </summary>
	ClusterBorder,
	/// <summary>
	/// Point is added to the core of a cluster, means it satisfies all criteria to be added to a cluster />
	/// </summary>
	ClusterCore,
}
