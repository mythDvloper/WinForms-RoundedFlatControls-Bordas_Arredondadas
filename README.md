# WinForms-RoundedFlatControls

🎨 **Controles personalizados para Windows Forms (.NET Framework 4.8)**

Este projeto contém diversos controles visuais com design minimalista e bordas arredondadas para aplicações WinForms.

---

## ⚙️ Como Usar

### ✅ Passo 1 - Compile o Projeto

Clone ou baixe este repositório e compile a solução no Visual Studio.

---

### ✅ Passo 2 - Crie um Projeto Windows Forms

Crie um novo projeto **Windows Forms Application (.NET Framework 4.8)**.

> ⚠️ **Atenção:**  
> Não funciona em .NET Core ou .NET 5/6/7.

---

### ✅ Passo 3 - Adicione a Referência

No seu projeto WinForms:

1. Clique com o botão direito em **Referências**.
2. Selecione **Adicionar Referência...**.
3. Clique em **Procurar...** e selecione o DLL compilado.

---

## 🪟 Como Deixar o Form com Bordas Arredondadas

1. **Defina a borda do formulário como `None`:**

   ```csharp
   this.FormBorderStyle = FormBorderStyle.None;
