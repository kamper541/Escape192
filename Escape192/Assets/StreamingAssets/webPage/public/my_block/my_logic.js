
let var_map = new Map()
var_map.set('x' , 0);
//TODO: operational symetic
    

Blockly.JavaScript['repeat'] = function(block) {
    var value_time = Blockly.JavaScript.valueToCode(block, 'time', Blockly.JavaScript.ORDER_ATOMIC);
    var statements_do = Blockly.JavaScript.statementToCode(block, 'do');
    // TODO: Assemble JavaScript into code variable.
    var code = `{name:"repeat",time:${value_time},do:[${statements_do}]},`;
    return code;
};

Blockly.JavaScript['varGame'] = function(block) {
    var dropdown_var = block.getFieldValue('Var');
    var value_name = Blockly.JavaScript.valueToCode(block, 'NAME', Blockly.JavaScript.ORDER_ATOMIC);
    // TODO: Assemble JavaScript into code variable.
    var code = value_name;
    console.log(code);
    return "";
};

Blockly.JavaScript['turn'] = function(block) {
    var dropdown_name = block.getFieldValue('NAME');
    // TODO: Assemble JavaScript into code variable.
    var code = `{"name":"turn","value":"${dropdown_name}"},`;
    console.log(code)
    return code;
};

Blockly.JavaScript['Climb'] = function(block) {
    // TODO: Assemble JavaScript into code variable.
    var code = `{"name":"climb"},`;
    return code;
};

Blockly.JavaScript['start_block'] = function(block) {
    var statements_name = Blockly.JavaScript.statementToCode(block, 'NAME');
    // TODO: Assemble JavaScript into code variable.
    var code = `@${statements_name}@`;
    return code;
};
Blockly.JavaScript['if_state'] = function (block) {
    var value_expression = Blockly.JavaScript.valueToCode(block, 'expression', Blockly.JavaScript.ORDER_ATOMIC);
    if(value_expression === ""){
        value_expression = `"None"`
    }
    // value_expression.splice(-1 , 1)
    var statements_do = Blockly.JavaScript.statementToCode(block, 'do');
    let x = String(statements_do)
    let y = x.substring(0 , x.length - 1)
    // TODO: Assemble JavaScript into code variable.
    var code = `{"name":"if","expression":${value_expression},"do":[${y}]},`;
    code = String(code)
    expr = []
    for (let i = 0 ; i < code.length ; i++){
        expr.push(code[i])
    }
    expr = expr.join("")
    return expr;
};

Blockly.JavaScript['turning_degree'] = function (block) {
    var value_block = Blockly.JavaScript.valueToCode(block, 'NAME', Blockly.JavaScript.ORDER_ATOMIC);
    // TODO: Assemble JavaScript into code variable.
    if(value_block === ""){
        value_block = 0
    }
    let val = block.getFieldValue('NAME')
    let id = 'Block'
    // return [id , value_block]
    // return "Turn(" + value_block + ")\n";
    return `{"name":"turn","value":${value_block}},`
};

Blockly.JavaScript['moving_unit'] = function (block) {
    var value_block = Blockly.JavaScript.valueToCode(block, 'Block', Blockly.JavaScript.ORDER_ATOMIC);
    // TODO: Assemble JavaScript into code variable.
    let test = `"${value_block}"`
    console.log(test);
    if(isNaN(test)){
        value_block = `"${value_block}"`
    }
    // var code = Blockly.JavaScript.workspaceToCode(workspace);
    if(value_block === ""){
        value_block = 0
    }
    let val = block.getFieldValue('Block')
    
    return `{"name":"move","value":${value_block}},`
};

Blockly.JavaScript['variables_set'] = function(block){
    var get_var = Blockly.JavaScript.variableDB_.getName(block.getFieldValue('FIELD_NAME'), Blockly.Variables.NAME_TYPE);

    var value = Blockly.JavaScript.valueToCode(block , 'set_to' , Blockly.JavaScript.ORDER_ATOMIC);
    var map_key = `${get_var[0]}`
    if(value == ""){
        value = 0
    }
    // console.log(value);
    // console.log(Blockly.getVariable());
    // TODO: fix default not to 0
    var_map.set(map_key,(value==undefined)? 0:value)
    // TODO: return code
    return `{name:"set_var_to",var:"${get_var}",value:${value}},`
}

Blockly.JavaScript['variables_get'] = function(block) {
    var get_var = Blockly.JavaScript.variableDB_.getName(block.getFieldValue('FIELD_NAME'), Blockly.Variables.NAME_TYPE);
    var myvar = [get_var , Blockly.JavaScript.ORDER_ATOMIC]
    var map_key = `${get_var[0]}`
    var value = get_var[1]
    if(!var_map.has(map_key)){
        var_map.set(map_key,(value==undefined)? 0:value)
    }
    return myvar
}

Blockly.JavaScript['step_on'] = function(block) {
  var dropdown_name = block.getFieldValue('Dropdown');
  // var code = `name:"step_on", value:${dropdown_name}`;
  //TODO: Fix This
  console.log(dropdown_name);
  return [dropdown_name, Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['if_step'] = function(block) {
    var dropdown_var = Blockly.JavaScript.valueToCode(block, 'if_val',  Blockly.JavaScript.ORDER_NONE);
    
    var statements_name = Blockly.JavaScript.statementToCode(block, 'NAME');
    // TODO: Assemble JavaScript into code variable.
    var code = `{name:"if", condition:"${dropdown_var}", do:[${statements_name}]},`;
    return code;
  };


function defined(){
    Blockly.JavaScript.INFINITE_LOOP_TRAP = null;
    var code = Blockly.JavaScript.workspaceToCode(workspace);
    // var what = code.split('\n'); //<---------- problem with "if"
    let x = String(code)
    // let y = x.substring(0 , x.length - 1)
    // console.log(code);
    let ans = []
    let check = false
    for ( i = 0 ; i < x.length ; i++){
        if(x[i] == "@" && check == false){
            check = true
        }
        else if(x[i] != "@" && check == true){
            ans.push(x[i])
        }else if(x[i] == "@" && check == true){
            check = false
        }
    }
    ans.pop()
    ans = ans.join("")
    if(ans == ''){
      ans = ans +`{name:"last"}`
    }else{
      ans = ans + `,{name:"last"}`
    }
    ans = `{name:"code", code:[` + ans + `]}`
    // ans = `{name:"code", code:[` + '{"name":"move","value":2},{"name":"turn","value":"left"},{"name":"move","value":2},{name:"last"}' + `]}`
    ans = '{payload:['+ getVariables() + ',' + ans +']}'
    console.log(ans);
    location.href= `code://${ans}?key=1&anotherKey=2`
    return `${ans}`;
  }


function createVariables(button){
    Blockly.Variables.createVariableButtonHandler(button.getTargetWorkspace(), null, 'var');
}

function getVariables(){
    let variable_by_blockly = Blockly.Variables.getAddedVariables(workspace , [])
    let map_from_web = var_map
    console.log(map_from_web);
    let ans = "";
    variable_by_blockly.forEach(element => {
        // if(map_from_web.has(element.name)){
            //     check_is_has = true
            //     let temp = map_from_web.get(element.name)
            //     map_to_unity += `{variable:"${element.name}",value:${(((typeof temp)=='string')? `"${temp}"`:temp)}},`
            // }
            ans = ans + `"${element.name}"` + ","
    });
    ans = ans.slice(0,-1)
    let map_to_unity = `{name:"var_map", map:[${ans}]}`
    // if(check_is_has){
    //     map_to_unity = map_to_unity.slice(0,-1)
    // }
    // map_to_unity += ']}'
    return map_to_unity
}
