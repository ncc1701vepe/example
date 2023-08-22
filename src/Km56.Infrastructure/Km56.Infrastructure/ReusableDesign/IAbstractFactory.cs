namespace Km56.Infrastructure.ReusableDesign
{
    /// <summary>
    /// The contract for an Abstract Factory
    /// </summary>
    /// <typeparam name="T">The Abstract Product</typeparam>
    public interface IAbstractFactory<T>
    {
        /// <summary>
        /// Creates a Product
        /// </summary>
        /// <returns></returns>
        T Create();
    }
}
