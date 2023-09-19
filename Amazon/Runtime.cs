namespace Amazon
{
    internal class Runtime
    {
        internal class BasicAWSCredentials
        {
            private string v1;
            private string v2;

            public BasicAWSCredentials(string v1, string v2)
            {
                this.v1 = v1;
                this.v2 = v2;
            }
        }
    }
}