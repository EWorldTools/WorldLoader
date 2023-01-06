using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldLoader.Utils;

public class MultiTextWriter : TextWriter
{
    private readonly List<TextWriter> _writers;

    public Action<string> MessageCallBack = new Action<string>((str) => File.AppendAllText("WorldLoader\\Log.txt", str));

    public MultiTextWriter(params TextWriter[] writers) =>
        _writers = writers.ToList();

    public MultiTextWriter(TextWriter writer) {
        _writers = new();
        _writers.Add(writer);
    }

    public override Encoding Encoding => Encoding.UTF8;

    public override void Write(char value) {
        foreach (TextWriter writer in _writers)
            writer.Write(value);
    }

    public override void Write(string value) {
        foreach (TextWriter writer in _writers)
            writer.Write(value);
        MessageCallBack?.Invoke(value);
    }

    public override void WriteLine(string value) {
        foreach (TextWriter writer in _writers)
            writer.WriteLine(value);
        MessageCallBack?.Invoke(value + "\n");
    }

    protected override void Dispose(bool disposing) {
        if (disposing)
            foreach (TextWriter writer in _writers)
                writer.Dispose();

        base.Dispose(disposing);
    }
}
