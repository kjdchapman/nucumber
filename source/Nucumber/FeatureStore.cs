using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Nucumber.Controllers
{
    // NOTE: As we build search/filter functionality into this class, we want to separate it from the "editing files on disk" functionality.
    public class FeatureStore : IEnumerable<FeatureFile>
    {
        private List<FeatureFile> _features;
        
        public FeatureStore(string featuresPath)
        {
            _features = new List<FeatureFile>();

            foreach (var featureFileName in Directory.GetFiles(featuresPath))
            {
                _features.Add(new FeatureFile
                {
                    Name = new FileInfo(featureFileName).Name,
                    RawText = File.ReadAllText(featureFileName)
                });
            }
        }

        public IEnumerator<FeatureFile> GetEnumerator()
        {
            return _features.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}