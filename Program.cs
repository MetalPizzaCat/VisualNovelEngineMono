#define RUN_PARSER_DEBUG

#if RUN_PARSER_DEBUG
DialogParser parser = new DialogParser("./test.diag");
#else
using var game = new VisualNovelMono.VisualNovelGame();
game.Run();
#endif