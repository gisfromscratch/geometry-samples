using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Spatial.Clustering.Data
{
    /// <summary>
    /// Represents a text file containing cluster points.
    /// </summary>
    public class ClusterPointTextFile : IEnumerable<ClusterPoint>
    {
        private readonly FileInfo _file;
        private readonly string _delimiter;
        private readonly uint _xTokenIndex;
        private readonly uint _yTokenIndex;
        private readonly uint _startRowIndex;

        /// <summary>
        /// Creates a new text file data source containing cluster points.
        /// </summary>
        /// <param name="file">The text file containing the cluster points.</param>
        /// <param name="delimiter">The token delimiter of this text file.</param>
        /// <param name="xTokenIndex">The token index of the x coordinate.</param>
        /// <param name="yTokenIndex">The token index of the y coordinate.</param>
        /// <param name="startRowIndex">The index of the first row. E.G. file with a header line has a start row index of <code>1</code>.</param>
        public ClusterPointTextFile(FileInfo file, string delimiter, uint xTokenIndex, uint yTokenIndex, uint startRowIndex)
        {
            if (null == file)
            {
                throw new ArgumentNullException(nameof(file));
            }
            if (!file.Exists)
            {
                throw new ArgumentException(@"The file does not exists!");
            }

            _file = file;
            _delimiter = delimiter;
            _xTokenIndex = xTokenIndex;
            _yTokenIndex = yTokenIndex;
            _startRowIndex = startRowIndex;
        }

        public IEnumerator<ClusterPoint> GetEnumerator()
        {
            return new TextFileEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TextFileEnumerator(this);
        }


        /// <summary>
        /// Represents an enumerator over a text file containing cluster points.
        /// </summary>
        private class TextFileEnumerator : IEnumerator<ClusterPoint>
        {
            private ClusterPointTextFile _textFile;
            private StreamReader _reader;
            private bool _streamEnded;
            private ClusterPoint _current;

            internal TextFileEnumerator(ClusterPointTextFile textFile)
            {
                _textFile = textFile;
                _reader = new StreamReader(_textFile._file.OpenRead());
                for (var rowIndex = 0; !_streamEnded && rowIndex < _textFile._startRowIndex; rowIndex++)
                {
                    if (null == _reader.ReadLine())
                    {
                        _streamEnded = true;
                        break;
                    }
                }
            }

            public ClusterPoint Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return _current; }
            }

            public void Dispose()
            {
                if (null != _reader)
                {
                    _reader.Dispose();
                    _reader = null;
                }
            }

            public bool MoveNext()
            {
                if (_streamEnded)
                {
                    return false;
                }

                var line = _reader.ReadLine();
                if (null == line)
                {
                    _streamEnded = true;
                    return false;
                }

                _current = new ClusterPoint();

                var splittedLine = line.Split(_textFile._delimiter.ToCharArray());
                if (_textFile._xTokenIndex < splittedLine.Length)
                {
                    double x;
                    if (double.TryParse(splittedLine[_textFile._xTokenIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out x))
                    {
                        _current.X = x;
                    }
                }
                if (_textFile._yTokenIndex < splittedLine.Length)
                {
                    double y;
                    if (double.TryParse(splittedLine[_textFile._yTokenIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out y))
                    {
                        _current.Y = y;
                    }
                }
                return true;
            }

            public void Reset()
            {
                Dispose();
                _reader = new StreamReader(_textFile._file.OpenRead());
            }
        }
    }
}
