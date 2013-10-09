using System;
using System.Collections.Generic;
using System.Linq;

namespace Seekit.Facets {
    internal class FacetContextMerger<T> {

        public void Merge(FacetsList<T> masterFacets, FacetsList<T> slaveFacets, bool includeEmptyFacets,Type type = null)
        {
            if (masterFacets == null || slaveFacets == null)
                return;

            var slavesToAdd = new List<Facet>();

            foreach (var slaveFacet in slaveFacets) {
                var master = masterFacets.Where(x => x.PropetyName == slaveFacet.PropetyName).SingleOrDefault();

                if (master == null && includeEmptyFacets) {
                    var clone = slaveFacet.Clone();

                    clone.FacetValues.ForEach(x => { 
                        x.TotalNumbersOfHits = x.Hits;
                        x.Hits = 0;
                    });

                    slavesToAdd.Add(clone);
                    
                }
                else if(master != null) {

                    foreach (var slaveFacetValue in slaveFacet.FacetValues)
                    {

                        var masterFv = master.FacetValues.Where(y => y.Name == slaveFacetValue.Name).SingleOrDefault();
                        if(masterFv == null) {
                            master.FacetValues.Add(new FacetValue{Hits = 0, Name = slaveFacetValue.Name, TotalNumbersOfHits = slaveFacetValue.Hits});
                        }
                        else {
                            masterFv.TotalNumbersOfHits = slaveFacetValue.Hits;
                        }
                    }
                }

            }
            masterFacets.AddRange(slavesToAdd);


        }
    }
}
