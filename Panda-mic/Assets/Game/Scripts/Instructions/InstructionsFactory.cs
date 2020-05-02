
namespace LineUp
{
    public class InstructionsFactory
    {
        /// <summary>
        /// Creates new instructions based on the difficulty.
        /// The smaller the difficulty number [0-100] the harder the instructions.
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public InstructionsConfiguration GetNewInstructions(int difficulty)
        {
            System.Random r = new System.Random();
            InstructionsConfiguration configuration = new InstructionsConfiguration
            {
                temperature = 36.6f + (r.Next(101) < difficulty ? 0f : UnityEngine.Random.Range(0.4f, 4f)),
                maskRequired = r.Next(101) > difficulty,
                glovesRequired = r.Next(101) > difficulty,
                symptoms = r.Next(101) > difficulty,
                requestIdRequired = r.Next(101) > difficulty,
                amountOfPeopleInside = r.Next(101) < difficulty ? UnityEngine.Random.Range(5, 10) : UnityEngine.Random.Range(2, 5)
            };

            return configuration;
        }
    }

    public struct InstructionsConfiguration
    {
        public float temperature;
        public bool maskRequired;
        public bool glovesRequired;
        public bool symptoms;
        ///<summary>When true a request along with the id are required.</summary>
        public bool requestIdRequired;
        ///<summary>The maximum amount of people that can be inside the supermarket.</summary>
        public int amountOfPeopleInside;

        public override string ToString()
        {
            return $"Temperature: {temperature:N1}, mask required: {maskRequired}, gloves required: {glovesRequired}, symptoms: {symptoms}, request id required: {requestIdRequired}, amount of people: {amountOfPeopleInside}";
        }
    }
}