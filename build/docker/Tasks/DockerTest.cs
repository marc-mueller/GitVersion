using Cake.Frosting;
using Common.Utilities;

namespace Docker.Tasks
{
    [TaskName(nameof(DockerTest))]
    [TaskDescription("Test the docker images containing the GitVersion Tool")]
    [TaskArgument(Arguments.DockerRegistry, Constants.DockerHub, Constants.GitHub)]
    [TaskArgument(Arguments.DockerDotnetVersion, Constants.Version50, Constants.Version31)]
    [TaskArgument(Arguments.DockerDistro, Constants.Alpine312, Constants.Debian10, Constants.Ubuntu2004)]
    [IsDependentOn(typeof(DockerBuild))]
    public class DockerTest : FrostingTask<BuildContext>
    {
        public override bool ShouldRun(BuildContext context)
        {
            var shouldRun = true;
            shouldRun &= context.ShouldRun(context.IsDockerOnLinux, $"{nameof(DockerTest)} works only on Docker on Linux agents.");

            return shouldRun;
        }

        public override void Run(BuildContext context)
        {
            foreach (var dockerImage in context.Images)
            {
                context.DockerTestImage(dockerImage);
            }
        }
    }
}
