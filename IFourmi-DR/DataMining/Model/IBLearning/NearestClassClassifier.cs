﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iFourmi.DataMining.Data;
using iFourmi.DataMining.Model;
using iFourmi.DataMining.Algorithms;
using iFourmi.DataMining.ClassificationMeasures;
using iFourmi.DataMining.ProximityMeasures;

namespace iFourmi.DataMining.Model.IBLearning
{
    public class NearestClassClassifier : AbstractLazyClassifier
    {
        public double SimilarityThreshold
        {
            get;
            set;
        }

        public NearestClassClassifier(IDistanceMeasure distanceMeasure, Dataset database, double[][] weights, double similarityThreshold)
            : base(distanceMeasure, database, weights)
        {
            this.SimilarityThreshold = similarityThreshold;
        }

        public NearestClassClassifier(IDistanceMeasure distanceMeasure, Dataset database, double[] weights, double similarityThreshold)
            : base(distanceMeasure, database, weights)
        {
      
            this.SimilarityThreshold = similarityThreshold;
        }



        public NearestClassClassifier(IDistanceMeasure distanceMeasure, Dataset database, double similarityThreshold)
            : base(distanceMeasure, database)
        {
            this.SimilarityThreshold = similarityThreshold;
        }


        public NearestClassClassifier(IDistanceMeasure distanceMeasure, Dataset database, double[][] weights)
            : base(distanceMeasure, database, weights)
        {
        }

        public NearestClassClassifier(IDistanceMeasure distanceMeasure, Dataset database, double[] weights)
            : base(distanceMeasure, database, weights)
        {
        }

        public NearestClassClassifier(IDistanceMeasure distanceMeasure, Dataset database)
            : base(distanceMeasure, database)
        {
        }

        public override Model.Prediction Classify(Data.Instance instance)
        {
            int[] classCount = new int[this._database.Metadata.Attributes.Length];
            double [] classSimilarity=new double[this._database.Metadata.Attributes.Length];

            for(int instanceIndex=0; instanceIndex<this._database.Size;instanceIndex++)
            {
                if (this._database[instanceIndex] == instance)
                    continue;
               
                Instance instancer=this._database[instanceIndex];
                double distance = this._distanceMeasure.CalculateDistance(instance, instancer, this._classBasedWeights);
                double similartiy = double.PositiveInfinity;
                if (distance > 0)
                    similartiy = 1 / distance;

                if (similartiy > this.SimilarityThreshold)
                {
                    classCount[instancer.Label] += 1;
                    classSimilarity[instancer.Label] += similartiy;
                }
            }

            int max = 0;
            for (int i = 0; i < classCount.Length; i++)
            {
                //if (classCount[i] != 0)
                  //  classSimilarity[i] /= classCount[i];

                if(classSimilarity[i]>classSimilarity[max])
                    max=i;
            }

            double sum = classSimilarity.Sum();
            for (int i = 0; i < classSimilarity.Length; i++)
                classSimilarity[i] /= sum;
            
            Prediction prediction=new Prediction(max,classSimilarity);

            return prediction;


        }
    }
}
