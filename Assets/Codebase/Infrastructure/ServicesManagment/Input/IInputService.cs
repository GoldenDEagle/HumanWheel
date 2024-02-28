namespace Assets.Codebase.Infrastructure.ServicesManagment.Input
{
    /// <summary>
    /// Manages input.
    /// </summary>
    public interface IInputService : IService
    {
        /// <summary>
        /// Enables input.
        /// </summary>
        public void EnableInput();

        /// <summary>
        /// Disables all input.
        /// </summary>
        public void DisableInput();


        // To be continued with needed input interactions...
    }
}
