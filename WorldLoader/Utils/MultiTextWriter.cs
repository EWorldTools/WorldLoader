using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldLoader.Utils;

public class MultiTextWriter : TextWriter
{
    private readonly IEnumerable<TextWriter> _writers;

    public MultiTextWriter(params TextWriter[] writers) =>
        _writers = writers;
    
    public override Encoding Encoding => Encoding.UTF8;

    public override void Write(char value) {
        foreach (TextWriter writer in _writers)
            writer.Write(value);
    }

    public override void Write(string value) {
        foreach (TextWriter writer in _writers)
            writer.Write(value);
    }

    public override void WriteLine(string value) {
        foreach (TextWriter writer in _writers)
            writer.WriteLine(value);
    }

    protected override void Dispose(bool disposing) {
        if (disposing)
            foreach (TextWriter writer in _writers)
                writer.Dispose();

        base.Dispose(disposing);
    }
}
