# REFACTOR_TODO

Este documento contém um plano priorizado e detalhado para refatorações e melhorias no projeto WinServicesTool. O objetivo é aumentar a manutenibilidade, testabilidade e segurança, com PRs pequenos e de baixo risco inicialmente.

## Objetivos

- Introduzir testes automatizados e CI para prevenir regressões.
- Reduzir acoplamento entre UI e lógica de negócio (melhor testabilidade).
- Tornar operações perigosas (registro/registro HKLM, elevação) mais seguras e testáveis.
- Diminuir I/O desnecessário (debounce de saves) e melhorar tratamento de erros.
- Aplicar regras de estilo do repositório (XMLDoc, sealed, naming, etc) de forma gradual.

## Regras operacionais para os PRs

- Cada PR deve ser pequeno, com uma mudança coerente e reversível.
- Incluir testes unitários para as mudanças que alterem comportamento.
- Documentar mudanças não triviais no README ou em comentários.
- Evitar mudanças de UI massivas em um único PR; preferir extrair uma interface/serviço primeiro.

---

## Backlog priorizado (ordenado por prioridade)

### PR-01: Infrastructure e tooling (baixo risco)

Objetivo: preparar a base para manter qualidade do código.

Tarefas:

- Adicionar `.editorconfig` com as regras principais (indent, trailing spaces, encoding).
- Adicionar target de `dotnet format` no repo (dev script) e uma checagem CI.
- Criar README.md (se necessário) com setup build/run/test.

Motivo: padronizar estilo e reduzir ruído em PRs futuros.

Tamanho estimado: 1–2 horas.

---

### PR-02: Projeto de testes inicial (baixo risco)

Objetivo: permitir testes unitários e TDD.

Tarefas:

- Criar `tests/WinServicesTool.Tests` com `xunit` + `Shouldly` + `NSubstitute` (conforme guideline do repo).
- Adicionar um teste que verifique que o handler do botão Start chama `IWindowsServiceManager.StartAsync` quando privilégio OK (mocando `IPrivilegeService`).
- Configurar o csproj de testes e adicionar referência ao projeto principal.

Motivo: Segurança para refactoramentos subsequentes.

Tamanho estimado: 3–6 horas.

---

### PR-03: GitHub Actions - build + test (baixo risco)

Objetivo: Garantir que build e testes rodem em PRs.

Tarefas:

- Adicionar workflow `ci.yml` em `.github/workflows/` com job Windows-latest que roda `dotnet build` e `dotnet test`.
- Incluir checagem de `dotnet format --check` opcional.

Tamanho estimado: 1–2 horas.

---

### PR-04: Abstração de acesso ao Registry (médio risco)

Objetivo: Isolar operações no registro em `IRegistryService` para teste e segurança.

Tarefas:

- Criar `Services/IRegistryService.cs` com operações usadas (`GetValue`, `SetValue`, `CreateSubKey`, `SetLastKeyForRegedit`, etc).
- Implementar `RegistryService` que utiliza `Microsoft.Win32.Registry`.
- Refatorar locais em `FormMain` e `WindowsServiceManager` para usar `IRegistryService`.
- Adicionar testes unitários para as chamadas (mocando a interface).

Motivo: Evitar acesso direto e facilitar mocks em testes.

Tamanho estimado: 4–8 horas.

---

### PR-05: Debounce para `ColumnWidthStore` (baixo/médio risco)

Objetivo: Evitar múltiplos writes para cada mudança de coluna.

Tarefas:

- Implementar debounce com `System.Timers.Timer` ou `Task` + `CancellationTokenSource` (por ex. 500ms).
- Fazer escrita assíncrona e atômica (salvar .tmp + renomear), com tratamento de falhas.
- Adicionar testes unitários simulando múltiplas mudanças rápidas.

Tamanho estimado: 2–4 horas.

---

### PR-06: Introduzir CancellationToken em IWindowsServiceManager (médio/alto risco)

Objetivo: Permitir cancelamento de operações long-running (start/stop/restart), e melhorar UX.

Tarefas:

- Atualizar `IWindowsServiceManager` para aceitar `CancellationToken` em métodos assíncronos.
- Propagar tokens dos handlers de UI para chamadas de serviço (adicionar um botão Cancel se necessário ou usar Disposable tokens que expiram ao fechar a operação).
- Atualizar implementações e testes.

Motivo: Melhor controle de operações e segurança.

Tamanho estimado: 6–16 horas.

---

### PR-07: Extrair ViewModel / Presenter para `FormMain` (alto risco)

Objetivo: Separação clara de UI e lógica para facilitar testes e evolução.

Tarefas:

- Criar `ViewModels/MainViewModel` (ou `Presenters/MainPresenter`) com operações expostas via métodos e INotifyPropertyChanged.
- Converter `FormMain` para delegar ações para o ViewModel e fazer binding.
- Cobrir com testes unitários.

Motivo: Escalabilidade e manutenção a longo prazo.

Tamanho estimado: 2–4 dias.

---

### PR-08: Hardening e auditoria de operações (alto risco)

Objetivo: Minimizar risco em operações destrutivas (registry changes, start/stop services).

Tarefas:

- Implementar backup automáticos de registry keys antes de alterá-las (opcionalmente export para .reg ou backup em HKLM snapshot).
- Adicionar logs detalhados e nivel de log configurável para operações sensíveis.
- Incluir opções de "dry-run" para aplicar alterações sem executar.

Tamanho estimado: 2–4 dias.

---

## Checklist para cada PR

- [ ] Pequeno e focado.
- [ ] Inclui testes unitários quando altera comportamento.
- [ ] Atualiza README quando muda o fluxo de build/run/test.
- [ ] Não quebra build (CI deve passar).

---

## Próximos passos imediatos

1. Implementar PR-01 (`.editorconfig` + `dotnet format` check). Este é o passo que você pediu que eu aguardasse antes de começar: vou segui-lo após este plano.
2. Implementar PR-02 (projeto de testes) em seguida.

Se desejar, eu posso implementar PR-01 agora (criar `.editorconfig` e adicionar um job de checagem no CI), ou começar por PR-02 (tests). Indique qual prefere que eu implemente primeiro.
