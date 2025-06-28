# WinForms-RoundedFlatControls

Controles para WinForms .NET Framework 4.8.

Para usar:

1 - Compile o projeto
2 - Crie um projeto Windows Forms .NET Framework 4.8 (não funciona no .NET)
3 - Adicione a referência

Para deixar o Form com bordas arredondadas: 
1 - De preferência deixe o form com BorderStyli = None (a função já irá deixar assim de qualquer forma)
2 - No Code-Behind, instancie a classe, exemplo: 
    "WinForms_RoundedFlatControls.Forms.RoundedForm RoundedForm = new WinForms_RoundedFlatControls.Forms.RoundedForm();"

3 - Chame a função no Load do formulário ou no construtor e passe o formulário como parâmetro, utilizando o exemplo acima:
    RoundedForm.SetRoundedRegion(this);

4 - Quando você rodar o projeto, o Form estará com as bordas arredondadas!
