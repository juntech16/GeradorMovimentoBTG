# Gerador de Movimento Browniano

Uma aplicação .NET MAUI para Windows que simula e visualiza o movimento browniano geométrico, um conceito usado em finanças para modelar o comportamento estocástico de preços de ativos.

## Funcionalidades

- Simulação de movimento browniano geométrico
- Visualização gráfica dinâmica
- Personalização de parâmetros:
  - Preço inicial
  - Volatilidade média
  - Retorno médio
  - Tempo (dias)
  - Número de simulações (até 10)
- Interface intuitiva com sliders e campos numéricos
- Legenda com cores para múltiplas simulações

## Tecnologias utilizadas

- .NET MAUI (Windows)
- Padrão MVVM
- GraphicsView/IDrawable para renderização do gráfico
- .NET 9.0

## Screenshot da aplicação

![Screenshot da aplicação](screenshot.png)

## Arquitetura

O projeto segue o padrão MVVM (Model-View-ViewModel):

- **Models**: Implementa a lógica de negócio para a geração do movimento browniano
- **ViewModels**: Conecta os models com a view, gerenciando os parâmetros e comandos
- **Views**: Interface gráfica com controles para entrada de dados e visualização dos resultados

## Como executar

1. Clone este repositório
2. Abra a solução no Visual Studio 2022 com suporte a .NET MAUI
3. Compile e execute o projeto para a plataforma Windows
