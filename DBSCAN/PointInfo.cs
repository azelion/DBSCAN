namespace Dbscan;

/// <summary>
/// A holding class for algorithm related information about a point.
/// </summary>
/// <typeparam name="T">The type of the element this object is holding.</typeparam>
public class PointInfo<T> : IPointData where T : IPointData
{
	/// <summary>
	/// Initializes a new <see cref="PointInfo{T}"/> with the object it is holding.
	/// </summary>
	/// <param name="item"></param>
	public PointInfo(T item) =>
		this.Item = item;

	/// <summary>
	/// The object being held by this holder
	/// </summary>
	public T Item { get; }

	/// <summary>
	/// What type of clustering is assigned to this point
	/// </summary>
	public ClusterType ClusterType { get; set; }

	/// <summary>
	/// The location of this point
	/// </summary>
	public Point Point => Item.Point;
}
