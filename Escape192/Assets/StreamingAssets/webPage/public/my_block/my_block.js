
// move
Blockly.Blocks['moving_unit'] = {
    init: function () {
        this.appendValueInput("Block")
            .setCheck("Number")
            .appendField("Moving");
        this.appendDummyInput()
            .appendField("Block(s)");
        this.setInputsInline(true);
        this.setPreviousStatement(true, null);
        this.setNextStatement(true, null);
        this.setColour(230);
        this.setTooltip("");
        this.setHelpUrl("");
    }
};

// turn
Blockly.Blocks['turning_degree'] = {
    init: function () {
        this.appendValueInput("NAME")
            .setCheck(null)
            .setAlign(Blockly.ALIGN_RIGHT)
            .appendField(new Blockly.FieldLabelSerializable("Turning"), "Turn");
        this.appendDummyInput()
            .appendField("Degrees");
        this.setInputsInline(true);
        this.setPreviousStatement(true, null);
        this.setNextStatement(true, null);
        this.setColour(210);
        this.setTooltip("");
        this.setHelpUrl("");
    }
};
// if
Blockly.Blocks['if_state'] = {
    init: function () {
        this.appendValueInput("expression")
            .setCheck(null)
            .appendField("if");
        this.appendDummyInput();
        this.appendStatementInput("do")
            .setCheck("Array")
        this.setColour(230);
        this.setPreviousStatement(true, null);
        this.setNextStatement(true, null);
        this.setTooltip("");
        this.setHelpUrl("");
    }
};


Blockly.Blocks['start_block'] = {
    init: function() {
      this.appendStatementInput("NAME")
          .setCheck(null)
          .appendField("Start");
      this.setColour(0);
   this.setTooltip("");
   this.setHelpUrl("");
   this.setDeletable(false);
    }
  };

Blockly.Blocks['jump'] = {
    init: function() {
      this.appendDummyInput()
          .appendField("Jump");
      this.setPreviousStatement(true, null);
      this.setNextStatement(true, null);
      this.setColour(230);
   this.setTooltip("");
   this.setHelpUrl("");
    }
  };

  Blockly.Blocks['turn'] = {
    init: function() {
      this.appendDummyInput()
          .appendField("Turn")
          .appendField(new Blockly.FieldDropdown([["Left","left"], ["Right","right"]]), "NAME");
      this.setPreviousStatement(true, null);
      this.setNextStatement(true, null);
      this.setColour(300);
   this.setTooltip("");
   this.setHelpUrl("");
    }
  };

  Blockly.Blocks['varGame'] = {
    init: function() {
      this.appendValueInput("NAME")
          .setAlign(Blockly.ALIGN_CENTRE)
          .appendField("Set Variable")
          .appendField(new Blockly.FieldVariable("x"), "FIELD_NAME")
          .appendField("to");
      this.setPreviousStatement(true, null);
      this.setNextStatement(true, null);
      this.setColour(0);
   this.setTooltip("");
   this.setHelpUrl("");
    }
  };

  Blockly.Blocks['repeat'] = {
    init: function() {
      this.appendValueInput("time")
          .setCheck(null)
          .appendField("repeat");
      this.appendDummyInput()
          .appendField("times");
      this.appendStatementInput("do")
          .setCheck(null)
          .appendField("do");
      this.setInputsInline(true);
      this.setPreviousStatement(true, null);
      this.setNextStatement(true, null);
      this.setColour(230);
   this.setTooltip("");
   this.setHelpUrl("");
    }
  };

  // Block for variable getter.
  Blockly.Blocks['variables_get'] = {
    init: function() {
      this.appendDummyInput()
        .appendField(new Blockly.FieldVariable("x"), "FIELD_NAME");
      this.setOutput(true, null);
      this.setColour(230);
    }
  };
  
  // Block for variable setter.
  Blockly.Blocks['variables_set'] = {
    init: function() {
      this.appendValueInput("set_to")
          .setCheck(null)
          .appendField("set")
          .appendField(new Blockly.FieldVariable("x"), "FIELD_NAME")
          .appendField("to");
      this.setPreviousStatement(true, null);
      this.setNextStatement(true, null);
      // this.setOutput(true, null);
      this.setColour(230);
    }
  };


