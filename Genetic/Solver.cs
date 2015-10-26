using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic
{
    public sealed class Solver
    {
        #region Fields
        private int mutationCounter;

        private bool mutate;
        #endregion

        #region Properties
        public int MutationRate
        {
            get;
            set;
        }
        public int GenerationSize
        {
            get;
            set;
        }
        public int GenerationsCount
        {
            get;
            set;
        }
        public bool NoMutations
        {
            get;
            set;
        }
        #endregion

        public Solver(int mutationRate, int generationSize, int generationsCount)
        {
            MutationRate = mutationRate;
            GenerationSize = generationSize;
            GenerationsCount = generationsCount;
        }

        private void ResetState()
        {
            mutationCounter = 0;
            mutate = false;
        }
        private void ComputeNextMutation()
        {
            if (NoMutations) return;

            if (mutationCounter >= MutationRate)
            {
                mutationCounter = 0;
                mutate = true;
            }
        }
        private void GenerateNextGeneration(ref List<Gene> genes)
        {
            if (genes.Count == 0)
            {
                while (genes.Count < GenerationSize) genes.Add(new Gene());

                return;
            }

            List<Gene> newGenes = new List<Gene>();

            for (int i = 0; i + 1 < genes.Count; i += 2)
            {
                ComputeNextMutation();

                Gene mother = genes[i];
                Gene father = genes[i + 1];

                Gene siblingA = new Gene(mother, father, mutate);
                Gene siblingB = new Gene(father, mother, mutate);

                if (siblingA.IsDesirableGene) newGenes.Add(siblingA);
                if (siblingB.IsDesirableGene) newGenes.Add(siblingB);

                if (mutate) mutate = false;
                
                mutationCounter++;
            }

            while (newGenes.Count < GenerationSize)
            {
                Gene gene = new Gene();

                if (gene.IsDesirableGene) newGenes.Add(gene);
            }

            genes = newGenes;
        }

        public List<Gene> Solve()
        {
            ResetState();

            List<Gene> bestGenes = new List<Gene>();
            List<Gene> genes = new List<Gene>();
            
            int generations = 0;
            Gene best = null;

            while (generations < GenerationsCount)
            {
                GenerateNextGeneration(ref genes);

                genes = genes.OrderByDescending(g => g.TotalValue).ToList();

                if (best == null || ((best.TotalValue < genes[0].TotalValue) && (best.TotalWeight > genes[0].TotalValue)))
                {
                    best = genes[0];
                    bestGenes.Add(best);
                }

                generations++; 
            }

            return genes;
        }
    }
}
