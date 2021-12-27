function display() {
    var tools = '<xml id="toolboxTest" style="display: none"><block type="moving_unit"></block></xml>'
    workspace = Blockly.inject("blocklyDiv", {
        comments: true,
        collapse: true,
        disable: true,
        grid: {
          spacing: 25,
          length: 3,
          colour: "#ccc",
          snap: true,
        },
        horizontalLayout: false,
        maxBlocks: Infinity,
        maxInstances: { test_basic_limit_instances: 3 },
        maxTrashcanContents: 256,
        // media: '../media/',
        oneBasedIndex: true,
        readOnly: false,
        rtl: rtl,
        move: {
          scrollbars: true,
          // scrollbars: false,
          drag: true,
          wheel: false,
        },
        toolbox: toolboxTest,
        toolboxPosition: "start",
        renderer: "geras",
        zoom: {
          // controls: true,
          // wheel: true,
          startScale: 0.8,
          // maxScale: 4,
          // minScale: 0.25,
          // scaleSpeed: 1.1
        },
      });

      var xml = Blockly.Xml.textToDom(
        '<xml><block type="start_block" x="20" y="20"></block></xml>'
      );
      xml.editable = false;
      xml.deletable = false;
      workspace.clear();
      workspace.registerButtonCallback("createVariable", createVariables);
      // workspace.registerButtonCallback('getVar', getVariables);
      Blockly.Xml.domToWorkspace(xml, workspace);
}

function defined() {
  Blockly.JavaScript.INFINITE_LOOP_TRAP = null;
  var code = Blockly.JavaScript.workspaceToCode(workspace);
  // var what = code.split('\n'); //<---------- problem with "if"
  let x = String(code);
  // let y = x.substring(0 , x.length - 1)
  // console.log(x[0]);
  let ans = [];
  let check = false;
  for (i = 0; i < x.length; i++) {
    if (x[i] == "@" && check == false) {
      check = true;
    } else if (x[i] != "@" && check == true) {
      ans.push(x[i]);
    } else if (x[i] == "@" && check == true) {
      check = false;
    }
  }
  ans.pop();
  ans = ans.join("");
  ans = `{"payload":[` + ans + `]}`;
  console.log(ans);
  location.href = `code://${ans}?key=1&anotherKey=2`;
  return `${ans}`;
}
