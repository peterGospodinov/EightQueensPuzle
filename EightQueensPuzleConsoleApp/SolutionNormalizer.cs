namespace EightQueensPuzleConsoleApp
{
    public class SolutionNormalizer
    {
        public string Normalize(int[] solution)
        {
            int n = solution.Length;
            var transformations = new List<string>();

            for (int i = 0; i < 4; i++)
            {
                solution = Rotate90(solution);
                transformations.Add(string.Join(",", solution));
                transformations.Add(string.Join(",", Reflect(solution)));
            }

            transformations.Sort();
            return transformations[0];
        }

        private int[] Rotate90(int[] solution)
        {
            int n = solution.Length;
            int[] rotated = new int[n];
            for (int i = 0; i < n; i++)
            {
                rotated[solution[i]] = n - 1 - i;
            }
            return rotated;
        }

        private int[] Reflect(int[] solution)
        {
            int n = solution.Length;
            int[] reflected = new int[n];
            for (int i = 0; i < n; i++)
            {
                reflected[i] = n - 1 - solution[i];
            }
            return reflected;
        }
    }
}
