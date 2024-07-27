# SocNet SSR

## Описание

### Приложения

- **soc-net-web** - приложение социальной сети

### Библиотеки

- **assets** - ассеты, то есть картинки и иконки.
- **pages** - страницы приложения по FSD.
- **shared** - по FSD.
- **styles** - стили для всего приложения. Читай README внутри.
- **widgets** - виджеты, по FSD - компоненты, которые используются разными страницами.

## Генерация кода

### Добавление библиотеки

Простая typescript библиотека

<code>
npx nx g @nx/js:lib --publishable --bundler=tsc --unitTestRunner=jest --projectNameAndRootFormat=derived --directory libs/<span style="color: crimson">name</span> --name=<span style="color: crimson">name</span> --importPath=@socnet/<span style="color: crimson">name</span> --dry-run
</code>
<br/>
<br/>
Библиотека Nextjs
<code>
npx nx g @nx/next:lib --bundler=tsc --publishable --style=scss --globalCss --directory libs/<span style="color: crimson">name</span> --unitTestRunner=jest --name=<span style="color: crimson">name</span> --importPath=@socnet/<span style="color: crimson">name</span>
</code>
