//#define RUN_PARSER_DEBUG

#if RUN_PARSER_DEBUG
DialogParser parser = new DialogParser("./test.diag");
Dialog dialog = parser.ParseDialog();
System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(dialog));
#else
using var game = new VisualNovelMono.VisualNovelGame();
game.Run();
#endif