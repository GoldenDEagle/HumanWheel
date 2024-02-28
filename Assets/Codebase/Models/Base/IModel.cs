namespace Assets.Codebase.Models.Base
{
    public interface IModel
    {
        /// <summary>
        /// Fills model with data (call after services bind)
        /// </summary>
        public void InitModel();
    }
}
