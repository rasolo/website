module.exports = () => {

const projectsTeaser = document.getElementById('projects-teaser');
const githubProjects = document.getElementById('github-projects');
const postsTeaser = document.getElementById('posts-teaser');
const latestPosts = document.getElementById('latest-posts');

if (!projectsTeaser || !githubProjects || !postsTeaser || !latestPosts) {
  return;
}

projectsTeaser.addEventListener('click', () => {
  githubProjects.scrollIntoView({ behavior: 'smooth'});
});

postsTeaser.addEventListener('click', () => {
  latestPosts.scrollIntoView({ behavior: 'smooth'});
});
};