using PIH.Core;
using Xunit;

namespace PIH.Tests;

public class ProxySignalEstimatorTests
{
    [Fact]
    public void EstimateCounterAttachment_DecreasesWithUnemploymentCounter()
    {
        Assert.Equal(1f, ProxySignalEstimator.EstimateCounterAttachment(0, 30f, 32));
        Assert.Equal(0.5f, ProxySignalEstimator.EstimateCounterAttachment(480, 30f, 32));
        Assert.Equal(0f, ProxySignalEstimator.EstimateCounterAttachment(960, 30f, 32));
    }

    [Fact]
    public void EstimateTimeAttachment_UsesExpectedHorizon()
    {
        Assert.Equal(1f, ProxySignalEstimator.EstimateTimeAttachment(0f));
        Assert.Equal(0.5f, ProxySignalEstimator.EstimateTimeAttachment(90f));
        Assert.Equal(0f, ProxySignalEstimator.EstimateTimeAttachment(180f));
    }

    [Fact]
    public void EstimateAdultAttachment_UsesWorkerStudentAndNonWorkerModes()
    {
        var worker = ProxySignalEstimator.EstimateAdultAttachment(true, false, 0f, 0f, 0.75f, 1f);
        var student = ProxySignalEstimator.EstimateAdultAttachment(false, true, 0f, 0f, 0.75f, 1f);
        var nonWorker = ProxySignalEstimator.EstimateAdultAttachment(false, false, 0.5f, 0.5f, 1f, 1f);

        Assert.Equal(1f, worker);
        Assert.Equal(0.2625f, student, 4);
        Assert.Equal(0.15f, nonWorker, 4);
    }

    [Fact]
    public void EstimateAttachmentScore_AveragesAdultAttachments()
    {
        float score = ProxySignalEstimator.EstimateAttachmentScore(1f, 0.5f, 0f);

        Assert.Equal(0.5f, score);
    }

    [Fact]
    public void EstimateHealthAndLifecycleDamping_UseExpectedDefaults()
    {
        Assert.Equal(1f, ProxySignalEstimator.EstimateHealthDamping(false, 0));
        Assert.Equal(0.75f, ProxySignalEstimator.EstimateHealthDamping(true, 0));
        Assert.Equal(0.75f, ProxySignalEstimator.EstimateHealthDamping(false, 1));

        Assert.Equal(1f, ProxySignalEstimator.EstimateLifecycleDamping(true, false));
        Assert.Equal(0.35f, ProxySignalEstimator.EstimateLifecycleDamping(true, true));
        Assert.Equal(0f, ProxySignalEstimator.EstimateLifecycleDamping(false, false));
    }
}
