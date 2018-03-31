namespace Genie.Core.Infrastructure.Interfaces
{
    /// <summary>
    /// This is a provider which should be used to give the
    /// target connection string to the context, this should be
    /// implemented inside the applicaiton.
    /// </summary>
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// Get the target connection string
        /// </summary>
        /// <returns>The connection string</returns>
        string GetConnectionString();
    }
}

