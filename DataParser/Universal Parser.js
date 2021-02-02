const fs = require("fs");
const uuid = require("uuid");

["governments"].forEach(parser);
function parser(fileName) {
    var itemIndex = -1;
    const items = [];
    const data = fs.readFileSync(`./data/${fileName}.txt`, "utf-8");
    data.split(/\r?\n/)
        .filter((line) => !line.startsWith("#") && line.trimLeft().length > 0)
        .map((line) => line.trim())
        .forEach((line) => {
            var match = line.match(/"([^"]+)"/);
            var hasQuotedName = match != null && match.index === 0;

            var rawName = hasQuotedName
                ? match[1]
                : line.substring(0, line.indexOf(" "));
            console.log(data);
        });
}
