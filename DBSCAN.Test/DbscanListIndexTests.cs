﻿using Xunit;

namespace Dbscan.Test;

public class DbscanListIndexTests
{
	#region Border Data Set
	[Fact]
	public void BorderTest1()
	{
		var clusters =
			Dbscan.CalculateClusters(
				DbscanTestData.Borders,
				1.0,
				4);

		Assert.Equal(2, clusters.Clusters.Count);
		Assert.Equal(1, clusters.UnclusteredObjects.Count);
		Assert.Equal(DbscanTestData.Borders[0], clusters.UnclusteredObjects[0]);

		Assert.Equal(4, clusters.Clusters[0].Objects.Count);
		Assert.Equal(4, clusters.Clusters[1].Objects.Count);
	}

	[Fact]
	public void BorderTest2()
	{
		var clusters =
			Dbscan.CalculateClusters(
				DbscanTestData.Borders,
				2.0,
				3);

		Assert.Equal(1, clusters.Clusters.Count);
		Assert.Equal(0, clusters.UnclusteredObjects.Count);
		Assert.Equal(9, clusters.Clusters[0].Objects.Count);
	}

	[Fact]
	public void BorderTest3()
	{
		var clusters =
			Dbscan.CalculateClusters(
				DbscanTestData.Borders,
				2.0,
				4);

		Assert.Equal(2, clusters.Clusters.Count);
		Assert.Equal(0, clusters.UnclusteredObjects.Count);

		Assert.Equal(5, clusters.Clusters[0].Objects.Count);
		Assert.Equal(4, clusters.Clusters[1].Objects.Count);
	}

	[Fact]
	public void BorderTest4()
	{
		var clusters =
			Dbscan.CalculateClusters(
				DbscanTestData.Borders,
				2.0,
				5);

		Assert.Equal(2, clusters.Clusters.Count);
		Assert.Equal(0, clusters.UnclusteredObjects.Count);

		Assert.Equal(5, clusters.Clusters[0].Objects.Count);
		Assert.Equal(4, clusters.Clusters[1].Objects.Count);
	}

	[Fact]
	public void BorderTest5()
	{
		var clusters =
			Dbscan.CalculateClusters(
				DbscanTestData.Borders,
				2.0,
				4);

		Assert.Equal(2, clusters.Clusters.Count);
		Assert.Equal(0, clusters.UnclusteredObjects.Count);

		Assert.Equal(5, clusters.Clusters[0].Objects.Count);
		Assert.Equal(4, clusters.Clusters[1].Objects.Count);
	}
	#endregion

	#region Ring Data Set
	[Fact]
	public void RingTest1()
	{
		var clusters =
			Dbscan.CalculateClusters(
				DbscanTestData.RingDataset,
				1.0,
				4);

		Assert.Equal(9, clusters.Clusters.Count);
		Assert.Equal(60, clusters.UnclusteredObjects.Count);
	}

	[Fact]
	public void RingTest2()
	{
		var clusters =
			Dbscan.CalculateClusters(
				DbscanTestData.RingDataset,
				1.01,
				4);

		Assert.Equal(12, clusters.Clusters.Count);
		Assert.Equal(45, clusters.UnclusteredObjects.Count);
	}

	[Fact]
	public void RingTest3()
	{
		var clusters =
			Dbscan.CalculateClusters(
				DbscanTestData.RingDataset,
				1.3,
				4);

		Assert.Equal(15, clusters.Clusters.Count);
		Assert.Equal(30, clusters.UnclusteredObjects.Count);
	}

	[Fact]
	public void RingTest4()
	{
		var clusters =
			Dbscan.CalculateClusters(
				DbscanTestData.RingDataset,
				0.99,
				2);

		Assert.Equal(15, clusters.Clusters.Count);
		Assert.Equal(45, clusters.UnclusteredObjects.Count);
	}
	#endregion
}
