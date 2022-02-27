// import * as CONST from "./const";
const number = '<block type="math_number"><field name="NUM">1</field></block>'
const move = `<block type="moving_unit"><value name="Block">${number}</value></block>`
const turn = '<block type="turn"></block>'
const climb = '<block type="Climb"></block>'
const movement_category = '<category name="Movement">' + move + turn + climb + '</category>'
const repeat = `<block type="repeat"><value name="time">${number}</value></block>`
const logic_category = '<category name="Logic">' + repeat + '</category>'
const create_variable = '<button text="create variable" callbackKey="createVariable"></button>'
const get_variable = '<block type="variables_get"></block>'
const set_variable = '<block type="variables_set"></block>'
const increase_variable = '<block type="variables_increase"></block>'
const variable_category = '<category name="Variable">' + create_variable + get_variable + set_variable + increase_variable + '</category>'
const if_step = '<block type="if_step"><value name="if_val"><block type="step_on"></block></value></block>'
// const if_step = '<block type="if_state"></block>'
const step_on = '<block type="step_on"></block>'
const statement_category = '<category name="Statement">' + if_step + "</category>"
// Custom requires for the playground.
const RenderTable = {
  1: move,
  2: move,
  3: move,
  4: move + turn,
  5: move + turn,
  6: move + turn,
  7: move + turn,
  8: move + turn,
  9: move + turn,
  10: move + turn,
  // 11: move + repeat,
  11: move + turn + repeat,
  // 12: move + turn + repeat,
  12: move + turn + repeat + if_step,
  13: move + turn + repeat,
  14: move + turn + repeat,
  15: move + turn + repeat,
  16: move + get_variable + set_variable,
  17: move + get_variable + set_variable,
  18: move + turn +  get_variable + set_variable,
  19: move + turn + create_variable + get_variable + set_variable,
  20: move + turn + create_variable + get_variable + set_variable + repeat,
  21: move + if_step,

  98: move + turn + if_step + climb,
  99: movement_category + logic_category + variable_category + statement_category
}
let workspace = null;
var match = location.search.match(/dir=([^&]+)/);
var rtl = match && match[1] == "rtl";

//TODO: Move RenderTable in to StageRequest.txt
function display(x) {
  // let param = JSON.stringify(x)
  // let request = param.split(",")
  // let ans = ""
  // console.log(x);
  // for (let i = 0 ; i < request.length; i++){
  //   ans += window[`${request[i]}`]
  // }
  let toolbox = document.getElementById("toolbox");
  toolbox.insertAdjacentHTML('afterbegin',RenderTable[99]);
  // toolbox.insertAdjacentHTML('afterbegin',RenderTable[x]);
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
  Blockly.Xml.domToWorkspace(xml, workspace);
}
