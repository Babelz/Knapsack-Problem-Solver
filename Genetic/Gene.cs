using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic
{
    public sealed class Gene
    {
        #region Private chromosome class
        private sealed class Chromosome
        {
            #region Fields
            public readonly int Weight;
            public readonly int Value;
            #endregion

            public Chromosome(int weight, int value)
            {
                Weight = weight;
                Value = value;
            }
        }
        #endregion

        #region Predefined chromosomes
        private static readonly Chromosome[] PredefinedChromosomes = new Chromosome[]
        {
            new Chromosome(5, 4),
            new Chromosome(1, 2),
            new Chromosome(4, 1),
            new Chromosome(1, 1),
            new Chromosome(2, 3),
            new Chromosome(2, 5),
            new Chromosome(2, 4),
            new Chromosome(4, 2),
            new Chromosome(6, 4),
            new Chromosome(3, 8),
            new Chromosome(6, 2),
            new Chromosome(2, 4),
            new Chromosome(1, 5),
            new Chromosome(3, 5),
            new Chromosome(3, 7),
            new Chromosome(4, 5),
            new Chromosome(3, 6),
            new Chromosome(9, 2),
            new Chromosome(5, 3)
        };
        #endregion

        #region Constants
        private const int MAX_CHROMOSOMES = 5;
        private const int MAX_WEIGHT      = 15;
        #endregion

        #region Fields
        private readonly Chromosome[] chromosomes;

        private bool isDesirableGene;
        
        private int totalWeight;
        private int totalValue;
        #endregion

        #region Properties
        public bool IsDesirableGene
        {
            get
            {
                return isDesirableGene;
            }
        }
        public int TotalWeight
        {
            get
            {
                return totalWeight;
            }
        }
        public int TotalValue
        {
            get
            {
                return totalValue;
            }
        }
        #endregion

        public Gene(Gene mother, Gene father, bool mutate)
        {
            // Inherit chromosomes from "parents".
            chromosomes = new Chromosome[MAX_CHROMOSOMES];

            chromosomes[0] = mother.chromosomes[0];
            chromosomes[1] = mother.chromosomes[1];

            chromosomes[2] = Genetic.Random.NextBool() ? mother.chromosomes[2] : father.chromosomes[2]; 

            chromosomes[3] = father.chromosomes[3];
            chromosomes[4] = father.chromosomes[4];

            if (mutate)
            {
                int firstToMutate = Genetic.Random.Next(0, MAX_CHROMOSOMES);
                int secondToMutate = Genetic.Random.Next(0, MAX_CHROMOSOMES);

                chromosomes[firstToMutate] = PredefinedChromosomes[Genetic.Random.Next(0, PredefinedChromosomes.Length)];
                chromosomes[secondToMutate] = PredefinedChromosomes[Genetic.Random.Next(0, PredefinedChromosomes.Length)];
            }

            ComputeWeight();
            ComputeValue();

            CheckIfIsDesirable();
        }
        public Gene()
        {
            // Select chromosomes by RNG.
            chromosomes = new Chromosome[MAX_CHROMOSOMES];

            chromosomes[0] = PredefinedChromosomes[Genetic.Random.Next(0, PredefinedChromosomes.Length)];
            chromosomes[1] = PredefinedChromosomes[Genetic.Random.Next(0, PredefinedChromosomes.Length)];
            chromosomes[2] = PredefinedChromosomes[Genetic.Random.Next(0, PredefinedChromosomes.Length)];
            chromosomes[3] = PredefinedChromosomes[Genetic.Random.Next(0, PredefinedChromosomes.Length)];
            chromosomes[4] = PredefinedChromosomes[Genetic.Random.Next(0, PredefinedChromosomes.Length)];

            ComputeWeight();
            ComputeValue();

            CheckIfIsDesirable();
        }

        private void ComputeWeight()
        {
            foreach (Chromosome chromosome in chromosomes) totalWeight += chromosome.Weight;
        }
        private void ComputeValue()
        {
            foreach (Chromosome chromosome in chromosomes) totalValue += chromosome.Value;
        }
        private void CheckIfIsDesirable()
        {
            isDesirableGene = totalWeight <= 15;
        }

        public override string ToString()
        {
            return string.Format("V: {0}\nW: {1}", totalValue, totalWeight);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
