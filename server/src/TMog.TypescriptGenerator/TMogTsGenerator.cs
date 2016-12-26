using TypeLite;
using TypeLite.TsModels;

namespace TMog.TypescriptGenerator
{
    public class TMogTsGenerator : TsGenerator
    {
        public TMogTsGenerator()
        {
            this.IndentationString = "";
        }

        protected override void AppendModule(TsModule module, ScriptBuilder sb, TsGeneratorOutput generatorOutput)
        {
            base.AppendModule(module, sb, generatorOutput);
        }

        protected override void AppendClassDefinition(TsClass classModel, ScriptBuilder sb, TsGeneratorOutput generatorOutput)
        {
            base.AppendClassDefinition(classModel, sb, generatorOutput);
        }

        protected override void AppendConstantModule(TsClass classModel, ScriptBuilder sb)
        {
            base.AppendConstantModule(classModel, sb);
        }

        protected override void AppendEnumDefinition(TsEnum enumModel, ScriptBuilder sb, TsGeneratorOutput output)
        {
            string typeName = this.GetTypeName(enumModel);
            string visibility = ((output & TsGeneratorOutput.Enums) == TsGeneratorOutput.Enums || (output & TsGeneratorOutput.Constants) == TsGeneratorOutput.Constants) ? "export " : "";
            this._docAppender.AppendEnumDoc(sb, enumModel, typeName);
            string constSpecifier = this.GenerateConstEnums ? "const " : string.Empty;
            sb.AppendLineIndented(string.Format("{0}{2}enum {1} {{", visibility, typeName, constSpecifier));
            using (sb.IncreaseIndentation())
            {
                int i = 1;
                foreach (TsEnumValue v in enumModel.Values)
                {
                    this._docAppender.AppendEnumValueDoc(sb, v);
                    sb.AppendLineIndented(string.Format((i < enumModel.Values.Count) ? "{0} = {1}," : "{0} = {1}", ToCamelCase(v.Name), v.Value));
                    i++;
                }
            }
            sb.AppendLineIndented("}");
            this._generatedEnums.Add(enumModel);
        }

        protected override void AppendReference(string reference, ScriptBuilder sb)
        {
            base.AppendReference(reference, sb);
        }

        private string ToCamelCase(string value)
        {
            return char.ToLower(value[0]) + value.Substring(1);
        }
    }
}
