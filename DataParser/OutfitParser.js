const fs = require("fs");
const uuid = require("uuid");

["engine", "harvesting", "outfit", "power", "weapon"].forEach(parser);

function parser(fileName) {
  var itemIdx = -1;
  const items = [];
  const data = fs.readFileSync(`./data/${fileName}.txt`, "UTF-8");
  const lines = data
    .split(/\r?\n/)
    .filter((line) => {
      return !line.startsWith("#") && line.trimLeft().length > 0;
    })
    .map((line) => {
      return line.trim();
    });

  lines.forEach((line) => {
    var match = line.match(/"([^"]+)"/);
    var hasQuotedName = match != null && match.index === 0;

    var rawName = hasQuotedName
      ? match[1]
      : line.substring(0, line.indexOf(" "));

    var name = rawName
      .replace(
        /\w\S*/g,
        (txt) => txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase()
      )
      .replace(" ", "");

    var value = line
      .substring(hasQuotedName ? rawName.length + 2 : rawName.length)
      .trim();

    if (name == "Outfit" || name == "Effect") {
      itemIdx++;
      items[itemIdx] = {};
      items[itemIdx] = {
        ...items[itemIdx],
        ID: uuid.v4(),
        Type: name,
      };
      name = "Name";
    }
    var finalValue =
      name === "Description" && items[itemIdx][name] != null
        ? (items[itemIdx][name] += value)
        : value;

    items[itemIdx] = {
      ...items[itemIdx],
      [name]: finalValue.replace(/"/g, "").replace(/\t/g, " "),
    };
    fs.writeFileSync(
      `./data/output/${fileName}-en.json`,
      JSON.stringify(
        {
          [`${fileName}Model`]: [...items],
        },
        null,
        4
      )
    );
  });
}
