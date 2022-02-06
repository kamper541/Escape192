// import * as CONST from "./const";
const move = '<block type="moving_unit"></block>'
const turn = '<block type="turn"></block>'
const jump = '<block type="jump"></block>'
const movement_category = '<category name="Movement">' + move + turn + jump + '</category>'
const number = '<block type="math_number"><field name="NUM">1</field></block>'
const repeat = '<block type="repeat"></block>'
const logic_category = '<category name="Logic">' + number + repeat + '</category>'
const create_variable = '<button text="create variable" callbackKey="createVariable"></button>'
const get_variable = '<block type="variables_get"></block>'
const set_variable = '<block type="variables_set"></block>'
const variable_category = '<category name="Variable">' + create_variable + + get_variable + set_variable + '</category>'
const if_statement = '<block type="if_state"></block>'
const step_on = '<block type="step_on"></block>'
const statement_category = '<category name="Statement">' + if_statement + step_on + "</category>"
// Custom requires for the playground.
const RenderTable = {
  1: number + move,
  2: number + move + turn,
  3: repeat + number + move + turn,
  4: movement_category + logic_category + variable_category + statement_category
}
let workspace = null;
var match = location.search.match(/dir=([^&]+)/);
var rtl = match && match[1] == "rtl";

//TODO: Upgrade to and API (Unit just pass what to render on webview)
function display(x) {
  let param = JSON.stringify(x)
  let request = param.split(",")
  let ans = ""
  for (let i = 0 ; i < request.length; i++){
    ans += window[`${request[i]}`]
  }
  let toolbox = document.getElementById("toolbox");
  toolbox.insertAdjacentHTML('afterbegin',RenderTable[4]);
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
    toolbox: toolbox,
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

// function defined() {
//   Blockly.JavaScript.INFINITE_LOOP_TRAP = null;
//   var code = Blockly.JavaScript.workspaceToCode(workspace);
//   // var what = code.split('\n'); //<---------- problem with "if"
//   let x = String(code);
//   // let y = x.substring(0 , x.length - 1)
//   // console.log(x[0]);
//   let ans = [];
//   let check = false;
//   for (i = 0; i < x.length; i++) {
//     if (x[i] == "@" && check == false) {
//       check = true;
//     } else if (x[i] != "@" && check == true) {
//       ans.push(x[i]);
//     } else if (x[i] == "@" && check == true) {
//       check = false;
//     }
//   }
//   ans.pop();
//   ans = ans.join("");
//   ans = `{"payload":[` + ans + `]}`;
//   console.log(ans);
//   location.href = `code://${ans}?key=1&anotherKey=2`;
//   return `${ans}`;
// }
